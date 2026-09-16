using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CustomerHandler
{
    internal class DeleteCustomerHandler(IUnitOfWork unitOfWork) : IRequestHandler<GenericDeleteCommand<CustomerDto, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<GenericResponse<string>> Handle(GenericDeleteCommand<CustomerDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity is null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            var customer = await _unitOfWork.customerRepository.GetByIdAsync(request.Entity.CustomerId, cancellationToken);
            if (customer is null)
            {
                return CreateErrorResponse($"No record found with the given Customer Id {request.Entity.CustomerId}", null);
            }

            await _unitOfWork.customerRepository.DeleteAsync(request.Entity.CustomerId, cancellationToken);
            await _unitOfWork.customerRepository.SaveAsync(cancellationToken);

            return new GenericResponse<string>
            {
                Message = "Account deleted successfully.",
                Success = true
            };
        }

        private GenericResponse<string> CreateErrorResponse(string message, string error)
        {
            return new GenericResponse<string>
            {
                Content = null,
                Error = error,
                Message = message,
                Success = false
            };
        }
    }
}

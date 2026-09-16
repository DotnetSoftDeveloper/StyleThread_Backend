using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper; // Ensure this matches your actual namespace
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CustomerHandler
{
    internal class UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GenericUpdateCommand<CustomerDto, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<CustomerDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            var customerData = await _unitOfWork.customerRepository.GetByIdAsync(request.Entity.CustomerId, cancellationToken);
            if (customerData == null)
            {
                return CreateErrorResponse($"No data found with the given Id {request.Entity.CustomerId}", "No Record Found");
            }

            _mapper.Map(request.Entity, customerData);
            await _unitOfWork.customerRepository.UpdateAsync(customerData);
            await _unitOfWork.customerRepository.SaveAsync(cancellationToken);

            return new GenericResponse<string>
            {
                Content = "Update Successfully",
                Message = "Data Updated Successfully",
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

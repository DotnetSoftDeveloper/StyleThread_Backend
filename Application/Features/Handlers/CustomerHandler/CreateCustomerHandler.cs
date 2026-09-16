using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CustomerHandler
{
    internal class CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<CustomerDto, GenericResponse<CustomerDto>>, GenericResponse<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GenericResponse<CustomerDto>> Handle(GenericCreateCommand<CustomerDto, GenericResponse<CustomerDto>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            var customer = _mapper.Map<Customer>(request.Entity);
            await _unitOfWork.customerRepository.InsertAsync(customer);
            await _unitOfWork.customerRepository.SaveAsync(cancellationToken);

            return new GenericResponse<CustomerDto>
            {
                Content = request.Entity,
                Message = "Customer created successfully",
                Success = true
            };
        }

        private GenericResponse<CustomerDto> CreateErrorResponse(string message, string error)
        {
            return new GenericResponse<CustomerDto>
            {
                Content = null,
                Error = error,
                Message = message,
                Success = false
            };
        }
    }
}

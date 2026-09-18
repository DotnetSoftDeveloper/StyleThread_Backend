using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Queries
{
    public class GetCustomerProfileQuery(int customerId) : IRequest<GenericResponse<CustomerDto>>
    {
        public int CustomerId { get; } = customerId;

        internal class GetCustomerProfileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            : IRequestHandler<GetCustomerProfileQuery, GenericResponse<CustomerDto>>
        {
            public async Task<GenericResponse<CustomerDto>> Handle(
                GetCustomerProfileQuery request,
                CancellationToken cancellationToken)
            {
                var customer = await unitOfWork.customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

                if (customer == null)
                {
                    return new GenericResponse<CustomerDto>
                    {
                        Success = false,
                        Message = "Customer profile was not found.",
                        Error = "No Record Found"
                    };
                }

                var customerDto = mapper.Map<CustomerDto>(customer);
                customerDto.CustomerId = customer.Id;
                customerDto.Password = string.Empty;

                return new GenericResponse<CustomerDto>
                {
                    Success = true,
                    Message = "Profile fetched successfully.",
                    Content = customerDto
                };
            }
        }
    }
}

using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class GetByIdHandler(IUnitOfWork _unitOfWork)
        : IRequestHandler<GenericFindByIdCommand<int, GenericResponse<List<CartDto>>>, GenericResponse<List<CartDto>>>
    {
        public async Task<GenericResponse<List<CartDto>>> Handle(GenericFindByIdCommand<int, GenericResponse<List<CartDto>>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    List<Cart> cartList = await _unitOfWork.cartRepository.GetCartByCustomerIdAsync(request.Entity, cancellationToken);
                    if (cartList.Any() == true)
                    {
                        var dto = cartList.Select ( cart => new CartDto
                        {
                            CartId = cart.CartId,
                            CustomerId = cart.CustomerId,
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                        }).ToList();

                        return new GenericResponse<List<CartDto>>
                        {
                            Content = dto,
                            Error = null,
                            Message = "Cart item retrieved successfully.",
                            Success = true
                        };
                    }

                    return new GenericResponse<List<CartDto>>
                    {
                        Content = null,
                        Error = "Not Found",
                        Message = $"No cart found with ID {request.Entity}",
                        Success = true
                    };
                }

                return new GenericResponse<List<CartDto>>
                {
                    Content = null,
                    Error = "Invalid Data",
                    Message = "Provided ID is null.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<CartDto>>
                {
                    Content = null,
                    Error = ex.InnerException?.Message ?? ex.Message,
                    Message = "An error occurred while retrieving the cart.",
                    Success = false
                };
            }
        }
    }
}

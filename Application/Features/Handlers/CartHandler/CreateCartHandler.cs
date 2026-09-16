using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class CreateCartHandler(IUnitOfWork _unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<CartDto, GenericResponse<CartDto>>, GenericResponse<CartDto>>
    {
        public async Task<GenericResponse<CartDto>> Handle(GenericCreateCommand<CartDto, GenericResponse<CartDto>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    Cart cart = mapper.Map<Cart>(request.Entity);
                    if (cart != null)
                    {
                        var existingCartItems = await _unitOfWork.cartRepository.GetCartByCustomerIdAsync(cart.CustomerId, cancellationToken);

                        var existingProduct = existingCartItems
                            .FirstOrDefault(x => x.ProductId == cart.ProductId);

                        if(existingProduct?.Quantity == 5)
                            return new GenericResponse<CartDto> { Content = null, Error = "Exceeding quantity", Message = "Exceed quantity of same product", Success = false };

                        if (existingProduct != null)
                        {
                            // Update the tracked entity directly
                            existingProduct.Quantity += cart.Quantity;

                            await _unitOfWork.cartRepository.UpdateAsync(existingProduct);
                        }
                        else
                            await _unitOfWork.cartRepository.InsertAsync(cart);

                        await _unitOfWork.cartRepository.SaveAsync(cancellationToken);

                        return new GenericResponse<CartDto> { Content = request.Entity, Error = "null", Message = "Item added successfully", Success = true };
                    }
                    return new GenericResponse<CartDto> { Content = null, Error = "Error occured", Message = "Error Occured while saving the data", Success = false };
                }
                return new GenericResponse<CartDto> { Content = null, Error = "Enter valid data", Message = "Please enter the valid data", Success = false };
            }
            catch (Exception ex)
            {
                return new GenericResponse<CartDto>
                {
                    Content = null,
                    Error = ex?.InnerException?.Message,
                    Message = ex?.Message,
                    Success = false
                };
            }
        }
    }
}

using Application.DTO;
using Application.Features.Command.GenericCommands;
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
    internal class UpdateCartHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GenericUpdateCommand<CartDto, GenericResponse<CartDto>>, GenericResponse<CartDto>>
    {
        public async Task<GenericResponse<CartDto>> Handle(GenericUpdateCommand<CartDto, GenericResponse<CartDto>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    Cart findCartItem = await _unitOfWork.cartRepository.GetByIdAsync(request.Entity.CartId, cancellationToken);
                    if (findCartItem != null)
                    {
                        var updateCartBinding = _mapper.Map(request.Entity, findCartItem);
                        await _unitOfWork.cartRepository.UpdateAsync(updateCartBinding);
                        await _unitOfWork.cartRepository.SaveAsync(cancellationToken);
                        return new GenericResponse<CartDto> { Content = request?.Entity, Error = "null", Message = "Cart Update Successfully", Success = true };
                    }
                    return new GenericResponse<CartDto> { Content = null, Error = "Error while finding the record", Message = $"No Record found with the given Brand Id {request.Entity.CartId}", Success = false };
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

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
    internal class DeleteCartHandler(IUnitOfWork _unitOfWork)
        : IRequestHandler<GenericDeleteByIdCommand<int, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericDeleteByIdCommand<int, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await _unitOfWork.cartRepository.GetByIdAsync(request.Id, cancellationToken);

                if (cartItem is null)
                {
                    return new GenericResponse<string>
                    {
                        Content = null,
                        Error = "Record not found",
                        Message = $"No cart item found with the given Id {request.Id}",
                        Success = false
                    };
                }

                await _unitOfWork.cartRepository.DeleteAsync(request.Id, cancellationToken);
                await _unitOfWork.cartRepository.SaveAsync(cancellationToken);

                return new GenericResponse<string>
                {
                    Content = null,
                    Error = null,
                    Message = "Cart item deleted successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
                {
                    Content = null,
                    Error = ex.InnerException?.Message ?? ex.Message,
                    Message = "An error occurred while deleting the cart item.",
                    Success = false
                };
            }
        }
    }
}

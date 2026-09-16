using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.ProductHandler
{
    public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<GenericDeleteCommand<ProductDto, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<GenericResponse<string>> Handle(GenericDeleteCommand<ProductDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            try
            {
                var product = await _unitOfWork.productRepository.GetByIdAsync(request.Entity.ProductId, cancellationToken);
                if (product == null)
                {
                    return CreateErrorResponse($"No record found with the given Product Id {request.Entity.ProductId}", null);
                }

                await _unitOfWork.productRepository.DeleteAsync(request.Entity.ProductId, cancellationToken);
                await _unitOfWork.productRepository.SaveAsync(cancellationToken);

                return new GenericResponse<string>
                {
                    Message = "Product deleted successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return CreateErrorResponse("An error occurred while processing your request.", ex.Message);
            }
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

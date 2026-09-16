using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CategoryHandler
{
    public class DeleteCategoryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GenericDeleteCommand<CategoryDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericDeleteCommand<CategoryDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                if(request?.Entity != null)
                {
                    var findCategoryById = await _unitOfWork.categoryRepository.GetByIdAsync(request.Entity.CategoryId, cancellationToken);
                    if (findCategoryById != null)
                    {
                        await _unitOfWork.categoryRepository.DeleteAsync(request.Entity.CategoryId, cancellationToken);
                        await _unitOfWork.categoryRepository.SaveAsync(cancellationToken);
                        return new GenericResponse<string>
                        {
                            Content = null,
                            Error = null,
                            Message = "Category delelted Successfully.",
                            Success = true
                        };
                    }
                    return new GenericResponse<string> { Content = null, Error = null, Message = $"No record found with the given Category Id {request.Entity.CategoryId}", Success = false };
                }
                return new GenericResponse<string> { Content = null, Error = "Enter valid data", Message = "Please enter the valid data", Success = false };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
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

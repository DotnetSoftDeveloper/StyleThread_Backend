using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper; // Ensure this matches your actual namespace
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CategoryHandler
{
    internal class UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GenericUpdateCommand<CategoryDto, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<CategoryDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            var category = await _unitOfWork.categoryRepository.GetByIdAsync(request.Entity.CategoryId, cancellationToken);
            if (category == null)
            {
                return CreateErrorResponse($"No record found with the given Category Id {request.Entity.CategoryId}", "Error while finding the record");
            }

            _mapper.Map(request.Entity, category);
            await _unitOfWork.categoryRepository.UpdateAsync(category);
            await _unitOfWork.categoryRepository.SaveAsync(cancellationToken);

            return new GenericResponse<string>
            {
                Content = "Updated Successfully",
                Message = "Category updated successfully",
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

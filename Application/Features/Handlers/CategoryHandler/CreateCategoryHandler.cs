using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CategoryHandler
{
    internal class CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<CategoryDto, GenericResponse<CategoryDto>>, GenericResponse<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GenericResponse<CategoryDto>> Handle(GenericCreateCommand<CategoryDto, GenericResponse<CategoryDto>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data");
            }

            var category = _mapper.Map<Category>(request.Entity);
            await _unitOfWork.categoryRepository.InsertAsync(category);
            await _unitOfWork.categoryRepository.SaveAsync(cancellationToken);

            return new GenericResponse<CategoryDto>
            {
                Content = request.Entity,
                Message = "Data added successfully",
                Success = true
            };
        }

        private GenericResponse<CategoryDto> CreateErrorResponse(string message, string error = "Error occurred while saving the data")
        {
            return new GenericResponse<CategoryDto>
            {
                Content = null,
                Error = error,
                Message = message,
                Success = false
            };
        }
    }
}

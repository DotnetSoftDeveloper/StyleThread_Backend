

using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.BrandHandler
{
    internal class UpdateBrandHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GenericUpdateCommand<BrandDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<BrandDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    Brand findBrandWithBrandId = await _unitOfWork.brandRepository.GetByIdAsync(request.Entity.BrandId, cancellationToken);
                    if (findBrandWithBrandId != null)
                    {
                        var updateBrandBinding = _mapper.Map(request.Entity, findBrandWithBrandId);
                        await _unitOfWork.brandRepository.UpdateAsync(updateBrandBinding);
                        await _unitOfWork.brandRepository.SaveAsync(cancellationToken);
                        return new GenericResponse<string> { Content = "Updated Successfully", Error = "null", Message = "Brand Update Successfully", Success = true };
                    }
                    return new GenericResponse<string> { Content = $"No Record found", Error = "Error while finding the record", Message = $"No Record found with the given Brand Id {request.Entity.BrandId}", Success = false };
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

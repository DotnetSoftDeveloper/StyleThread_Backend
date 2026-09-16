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

namespace Application.Features.Handlers.BrandHandler
{
    public class DeleteBrandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GenericDeleteCommand<BrandDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericDeleteCommand<BrandDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
			try
			{
				if(request?.Entity != null)
				{
					var findBrandForDelete = await _unitOfWork.brandRepository.GetByIdAsync(request.Entity.BrandId, cancellationToken);
					if(findBrandForDelete != null)
					{
						await _unitOfWork.brandRepository.DeleteAsync(request.Entity.BrandId, cancellationToken);
						await _unitOfWork.brandRepository.SaveAsync(cancellationToken);
						return new GenericResponse<string> { Content = null, Error = null, Message = "Brand Deleted Successfully.", Success = true };
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

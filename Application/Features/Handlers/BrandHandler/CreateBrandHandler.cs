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

namespace Application.Features.Handlers.BrandHandler
{
    internal class CreateBrandHandler(IUnitOfWork _unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<BrandDto, GenericResponse<BrandDto>>, GenericResponse<BrandDto>>
    {
        public async Task<GenericResponse<BrandDto>> Handle(GenericCreateCommand<BrandDto, GenericResponse<BrandDto>> request, CancellationToken cancellationToken)
        {
            try
            {
                if(request?.Entity != null)
                {
                    Brand customer = mapper.Map<Brand>(request.Entity);
                    if(customer != null)
                    {
                        await _unitOfWork.brandRepository.InsertAsync(customer);
                        await _unitOfWork.brandRepository.SaveAsync(cancellationToken);
                        return new GenericResponse<BrandDto> { Content = request.Entity, Error = "null", Message = "Brand Created Successfully", Success = true };
                    }
                    return new GenericResponse<BrandDto> { Content = null, Error = "Error occured", Message = "Error Occured while saving the data", Success = false };
                }
                return new GenericResponse<BrandDto> { Content = null, Error = "Enter valid data", Message = "Please enter the valid data", Success = false };
            }
            catch (Exception ex)
            {
                return new GenericResponse<BrandDto>
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

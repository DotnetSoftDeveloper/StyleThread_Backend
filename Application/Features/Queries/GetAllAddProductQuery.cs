using Application.DTO;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Queries
{
    public class GetAllAddProductQuery : IRequest<GenericResponse<CompositeResponse>>
    {
        internal class GetAllAddProductQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllAddProductQuery, GenericResponse<CompositeResponse>>
        {
            public async Task<GenericResponse<CompositeResponse>> Handle(GetAllAddProductQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Domain.Entities.Category> categories = await _unitOfWork.categoryRepository.GetAllAsync(cancellationToken);
                IEnumerable<Domain.Entities.Brand> brands = await _unitOfWork.brandRepository.GetAllAsync(cancellationToken);
                var fits = await _unitOfWork.fitRepository.GetAllAsync(cancellationToken);
                var fabrics = await _unitOfWork.fabricRepository.GetAllAsync(cancellationToken);
                var sleeves = await _unitOfWork.sleeveRepository.GetAllAsync(cancellationToken);
                var neckTypes = await _unitOfWork.neckTypeRepository.GetAllAsync(cancellationToken);
                var fabricCares = await _unitOfWork.fabricCareRepository.GetAllAsync(cancellationToken);
                var color = await _unitOfWork.colorRepository.GetAllAsync(cancellationToken);
                var sizes = await _unitOfWork.sizeRepository.GetAllAsync(cancellationToken);

                var compositeResponse = new CompositeResponse
                {
                    Categories = categories.ToList(),
                    Brands = brands.ToList(),
                    Fits = fits.ToList(),
                    Fabrics = fabrics.ToList(),
                    Sleeves = sleeves.ToList(),
                    NeckTypes = neckTypes.ToList(),
                    FabricCares = fabricCares.ToList(),
                    Color = color.ToList(),
                    Sizes = sizes.ToList()
                };

                var response = new GenericResponse<CompositeResponse>
                {
                    Message = "Data fetched successfully",
                    Error = null,
                    Content = compositeResponse,
                    Success = true
                };

                return response;
            }
        }
    }
}

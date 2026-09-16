using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Ensure this namespace is included for Any() extension method
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Queries
{
    public class GetAllProductsQuery : IRequest<GenericResponse<IEnumerable<ProductDto>>>
    {
        internal class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GenericResponse<IEnumerable<ProductDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<GenericResponse<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                // Fetch all products along with their variants and sizes in a single optimized query
                var productData = await _unitOfWork.productRepository.GetAllWithVariantsAndSizesAsync(cancellationToken);
                var sizeData = await _unitOfWork.sizeRepository.GetAllAsync(cancellationToken);

                // Create a dictionary for quick lookup of sizes
                var sizeDictionary = sizeData.ToDictionary(s => s.SizeId, s => s.SizeName);

                // Ensure data is mapped properly
                var productRecord = _mapper.Map<IEnumerable<ProductDto>>(productData) ?? Enumerable.Empty<ProductDto>();

                //Use LINQ to map size
                productRecord = productRecord.Select(product =>
                {
                    product.ProductVariants = product.ProductVariants.Select(variant =>
                    {
                        variant.ProductVariantSizes = variant.ProductVariantSizes.Select(variantSize =>
                        {
                            variantSize.Size = new SizeDto
                            {
                                SizeId = variantSize.SizeId,
                                SizeName = sizeDictionary.TryGetValue(variantSize.SizeId, out var sizeName) ? sizeName : ""
                            };
                            return variantSize;
                        }).ToList();
                        return variant;
                    }).ToList();
                    return product;
                }).ToList();

                var response = new GenericResponse<IEnumerable<ProductDto>>
                {
                    Message = "Data fetched successfully",
                    Error = null,
                    Content = productRecord,
                    Success = true
                };

                return response;
            }
        }
    }
}

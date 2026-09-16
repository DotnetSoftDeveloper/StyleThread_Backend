using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class GetAllBrandsQuery : IRequest<IEnumerable<Brand>>
    {
        internal class GetAllProductsQureyHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>
        {
            public async Task<IEnumerable<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.brandRepository.GetAllAsync(cancellationToken);
            }
        }
    }
}

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
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        internal class GetAllCategoriesQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
        {
            public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.categoryRepository.GetAllAsync(cancellationToken);
            }
        }
    }
}

using Application.Interfaces;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
        internal class GetAllProductsQureyHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
        {
            public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.customerRepository.GetAllAsync(cancellationToken);
            }
        }
    }
}

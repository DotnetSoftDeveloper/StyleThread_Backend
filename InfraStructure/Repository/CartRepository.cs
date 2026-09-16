using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    internal class CartRepository(ApplicationDbContext _dbContext) : Repository<Cart>(_dbContext), ICartRepository
    {
        public async Task<List<Cart>> GetCartByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
        {
            return _dbContext.Cart.Where(x=>x.CustomerId == customerId).ToList();
        }
    }
}

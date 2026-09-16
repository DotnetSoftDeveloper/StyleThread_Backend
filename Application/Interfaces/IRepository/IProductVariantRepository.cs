using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IProductVariantRepository : IRepository<ProductVariant>  
    {
        Task<ICollection<ProductVariant>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<bool> UpdateProductVariantAsync(IEnumerable<ProductVariant> productVariants, CancellationToken cancellationToken);
    }
}

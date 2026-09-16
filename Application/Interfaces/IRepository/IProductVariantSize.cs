using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IProductVariantSize 
    {
        Task<ICollection<ProductVariantSize>> GetByProductVariantSizeIdAsync(int productVariantId, CancellationToken cancellationToken);
        Task<bool> UpdateProductVariantSizeAsync(IEnumerable<ProductVariantSize> productVariantsizes, CancellationToken cancellationToken);
    }
}

using Application.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    internal class ProductVariantSize(ApplicationDbContext dbContext) : IProductVariantSize
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<ICollection<Domain.Entities.ProductVariantSize>> GetByProductVariantSizeIdAsync(int productVariantId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.ProductVariantSizes
                .Where(variant => variant.ProductVariantId == productVariantId)
                .ToListAsync(cancellationToken);

            return data ?? new List<Domain.Entities.ProductVariantSize>();
        }

        public async Task<bool> UpdateProductVariantSizeAsync(IEnumerable<Domain.Entities.ProductVariantSize> productVariantsizes, CancellationToken cancellationToken)
        {
            _dbContext.ProductVariantSizes.UpdateRange(productVariantsizes);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}

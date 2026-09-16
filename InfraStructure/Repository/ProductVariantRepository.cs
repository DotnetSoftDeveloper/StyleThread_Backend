using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;
using InfraStructure.Repository;


namespace InfraStructure.Repository
{
    internal class ProductVariantRepository(ApplicationDbContext _dbContext) : Repository<ProductVariant>(_dbContext), IProductVariantRepository
    {
        public async Task<ICollection<ProductVariant>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.ProductVariants
                .Where(variant => variant.ProductId == productId)
                .ToListAsync(); // Ensure you are using ToListAsync
            return data;
        }
        public async Task<bool> UpdateProductVariantAsync(IEnumerable<ProductVariant> productVariants, CancellationToken cancellationToken)
        {
            _dbContext.ProductVariants.UpdateRange(productVariants);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }



    }
}

using Application.Interfaces.IRepository;
using Domain.Entities;
using InfraStructure.Repository;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;


namespace InfraStructure.Repository
{
    internal class ProductRepository(ApplicationDbContext _dbContext) : Repository<Product>(_dbContext), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllWithVariantsAndSizesAsync(CancellationToken cancellationToken)
        {
            var products = await _dbContext.Product
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductVariantSizes)
                .ToListAsync(cancellationToken);

            // Manually map the SizeId to Size (perhaps in a service layer or mapping)
            //foreach (var product in products)
            //{
            //    foreach (var variant in product.ProductVariants)
            //    {
            //        foreach (var variantSize in variant.ProductVariantSizes)
            //        {
            //            var size = await _dbContext.Sizes
            //                .FirstOrDefaultAsync(s => s.SizeId == variantSize.SizeId, cancellationToken);
            //            variantSize.Size = size; // Manually set the Size entity based on SizeId
            //        }
            //    }
            //}

            return products;
        }


    }
}

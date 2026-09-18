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
            return await _dbContext.Product
                .AsNoTracking()
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductVariantSizes)
                .OrderByDescending(p => p.ListedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> SearchWithVariantsAndSizesAsync(string searchTerm, CancellationToken cancellationToken)
        {
            var normalizedTerm = EscapeLikePattern(searchTerm.Trim());
            var searchPattern = $"%{normalizedTerm}%";

            return await _dbContext.Product
                .AsNoTracking()
                .Where(product =>
                    EF.Functions.Like(product.Name, searchPattern, @"\") ||
                    EF.Functions.Like(product.SKU, searchPattern, @"\") ||
                    EF.Functions.Like(product.Description, searchPattern, @"\") ||
                    EF.Functions.Like(product.Category.Name, searchPattern, @"\") ||
                    EF.Functions.Like(product.Brand.Name, searchPattern, @"\") ||
                    EF.Functions.Like(product.Fit.FitName, searchPattern, @"\") ||
                    EF.Functions.Like(product.Fabric.FabricName, searchPattern, @"\") ||
                    EF.Functions.Like(product.Sleeve.SleeveType, searchPattern, @"\") ||
                    EF.Functions.Like(product.NeckType.NeckTypeName, searchPattern, @"\") ||
                    EF.Functions.Like(product.FabricCare.CareInstructions, searchPattern, @"\") ||
                    product.ProductVariants.Any(variant =>
                        EF.Functions.Like(variant.Color.ColorName, searchPattern, @"\") ||
                        _dbContext.Sizes.Any(size =>
                            variant.ProductVariantSizes.Any(variantSize => variantSize.SizeId == size.SizeId) &&
                            EF.Functions.Like(size.SizeName, searchPattern, @"\"))))
                .Include(product => product.ProductVariants)
                    .ThenInclude(variant => variant.ProductVariantSizes)
                .OrderByDescending(product => product.ListedOn)
                .ToListAsync(cancellationToken);
        }

        private static string EscapeLikePattern(string value)
        {
            return value
                .Replace(@"\", @"\\")
                .Replace("%", @"\%")
                .Replace("_", @"\_")
                .Replace("[", @"\[");
        }
    }
}

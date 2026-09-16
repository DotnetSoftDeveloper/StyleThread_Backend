using Application.Interfaces.IRepository;
using InfraStructure.Repository;
using InfraStructure.Context;

namespace InfraStructure.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            brandRepository = new BrandRepository(_context);
            categoryRepository = new CategoryRepository(_context);
            customerRepository = new CustomerRepository(_context);
            productRepository = new ProductRepository(_context);
            productVariantRepository = new ProductVariantRepository(_context);
            fitRepository = new FitRepository(_context);
            fabricCareRepository = new FabricCareRepository(_context);
            sleeveRepository = new SleeveRepository(_context);
            fabricRepository = new FabricRepository(_context);
            neckTypeRepository = new NeckTypeRepository(_context);
            colorRepository = new ColorRepository(_context);
            sizeRepository = new SizeRepository(_context);
            productVariantSize = new ProductVariantSize(_context);
            cartRepository = new CartRepository(_context);
            emailOtpRepository = new EmailOtpRepository(_context);
        }
        public IBrandRepository brandRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }
        public ICustomerRepository customerRepository { get; private set; }
        public IProductRepository productRepository { get; private set; }

        public IProductVariantRepository productVariantRepository { get; private set; }

        public IFitRepository fitRepository { get; private set; }

        public IFabricRepository fabricRepository { get; private set; }

        public IFabricCareRepository fabricCareRepository { get; private set; }

        public ISleeveRepository sleeveRepository { get; private set; }

        public INeckTypeRepository neckTypeRepository { get; private set; }

        public IColorRepository colorRepository { get; private set; }

        public ISizeRepository sizeRepository { get; private set; }

        public IProductVariantSize productVariantSize { get; private set; }
        public ICartRepository cartRepository { get; private set; }

        public IEmailOtpRepository emailOtpRepository { get; private set; }
    }
}

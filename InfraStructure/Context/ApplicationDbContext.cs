using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<Customer, Role, int>, IApplicationDbcontext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantSize> ProductVariantSizes { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Fit> Fits { get; set; }
        public DbSet<Sleeve> Sleeves { get; set; }
        public DbSet<NeckType> NeckTypes { get; set; }
        public DbSet<FabricCare> FabricCares { get; set; }
        public DbSet<EmailOtp> EmailOtps { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation)
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<ProductVariant>()
                .HasKey(pv => pv.ProductVariantId);

            modelBuilder.Entity<Fabric>()
                .HasKey(f => f.FabricId);

            modelBuilder.Entity<ProductVariantSize>()
                .HasKey(pvs => new { pvs.ProductVariantId, pvs.SizeId });

            modelBuilder.Entity<ProductVariantSize>()
                .HasOne(pvs => pvs.ProductVariant)
                .WithMany(pv => pv.ProductVariantSizes)
                .HasForeignKey(pvs => pvs.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure cascading delete is handled correctly

            modelBuilder.Entity<CustomerAddress>()
                .HasKey(address => address.CustomerAddressId);

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(address => address.Customer)
                .WithMany(customer => customer.CustomerAddresses)
                .HasForeignKey(address => address.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerAddress>()
                .HasIndex(address => new { address.CustomerId, address.IsDefault });

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Sleeve>().HasData(SeedData.GetSleeves());
            modelBuilder.Entity<Fit>().HasData(SeedData.GetFits());
            modelBuilder.Entity<Size>().HasData(SeedData.GetSizes());
            modelBuilder.Entity<NeckType>().HasData(SeedData.GetNeckTypes());
            modelBuilder.Entity<FabricCare>().HasData(SeedData.GetFabricCares());
            modelBuilder.Entity<Category>().HasData(SeedData.GetCategories());
            modelBuilder.Entity<Fabric>().HasData(SeedData.GetFabrics());
            modelBuilder.Entity<Color>().HasData(SeedData.GetColors());
        }

    }
}

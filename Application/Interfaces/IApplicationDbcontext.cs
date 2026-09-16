using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbcontext 
    {
        DbSet<Product> Product { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Brand> Brand { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderItem> OrderItem { get; set; }
        DbSet<Cart> Cart { get; set; }
        DbSet<Wishlist> Wishlist { get; set; }
        DbSet<Review> Review { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

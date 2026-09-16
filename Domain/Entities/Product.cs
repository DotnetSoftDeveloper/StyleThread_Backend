using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Address { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public DateTime ListedOn { get; set; }
        public string ListedBy { get; set; }
        public int FitId { get; set; }
        public int FabricId { get; set; }
        public int SleeveId { get; set; }
        public bool Reversible { get; set; }
        public int NeckTypeId { get; set; }
        public int FabricCareId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Fit Fit { get; set; }
        public virtual Sleeve Sleeve { get; set; }
        public virtual NeckType NeckType { get; set; }
        public virtual FabricCare FabricCare { get; set; }
        public virtual Fabric Fabric { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
    public class ProductVariant
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int Discount { get; set; }
        public int Inventory { get; set; }
        public List<string> Image { get; set; }

        public virtual Color Color { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public virtual ICollection<ProductVariantSize> ProductVariantSizes { get; set; }
    }
    public class Color
    {
        public int ColorId { get; set; }
        public string  ColorName { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
    public class Fabric
    {
        public int FabricId { get; set; }
        public string FabricName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class FabricCare
    {
        public int FabricCareId { get; set; }
        public string CareInstructions { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class NeckType
    {
        public int NeckTypeId { get; set; }
        public string NeckTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    public class Sleeve
    {
        public int SleeveId { get; set; }
        public string SleeveType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    public class Fit
    {
        public int FitId { get; set; }
        public string FitName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    public class Size
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
    }
    public class ProductVariantSize
    {
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        public int SizeId { get; set; }
    }


    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }


    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }



    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Cart
    {
        public int CartId { get; set; } 
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }

    public class Review
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}

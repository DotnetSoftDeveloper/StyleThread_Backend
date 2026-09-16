
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using static Application.DTO.Auth;

namespace Application.DTO
{

    public class ProductDto
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
        public ICollection<ProductVariantDto> ProductVariants { get; set; }
    }

    public class ProductVariantDto
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int Discount { get; set; }
        public int Inventory { get; set; }
        public List<string> Image { get; set; }
        public ICollection<ProductVariantSizeDto> ProductVariantSizes { get; set; } // Ensure proper mapping
    }

    public class ProductVariantSizeDto
    {
        public int ProductVariantId { get; set; }
        public int SizeId { get; set; }
        public SizeDto Size { get; set; } // Ensure Size object is fully mapped
    }

    public class SizeDto
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
    }

    public class ColorDto
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
    public class FabricDto
    {
        public int FabricId { get; set; }
        public string FabricName { get; set; }
    }
    public class FabricCareDto
    {
        public int FabricCareId { get; set; }
        public string CareInstructions { get; set; }
    }
    public class NeckTypeDto
    {
        public int NeckTypeId { get; set; }
        public string NeckTypeName { get; set; }
    }
    public class SleeveDto
    {
        public int SleeveId { get; set; }
        public string SleeveType { get; set; }
    }
    public class FitDto
    {
        public int FitId { get; set; }
        public string FitName { get; set; }
    }
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class BrandDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }  // Assuming CustomerId is a string
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }


    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }


    public class CartDto
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }  // Assuming CustomerId is a string
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


    public class WishlistDto
    {
        public int WishlistId { get; set; }
        public string CustomerId { get; set; }  // Assuming CustomerId is a string
        public int ProductId { get; set; }
    }


    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string CustomerId { get; set; }  // Assuming CustomerId is a string
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }

}

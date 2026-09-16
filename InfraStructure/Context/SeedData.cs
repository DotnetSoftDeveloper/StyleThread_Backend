using Domain.Entities;

namespace InfraStructure.Context
{
    public static class SeedData
    {
        public static List<Sleeve> GetSleeves() => new List<Sleeve>
    {
        new Sleeve { SleeveId = 1, SleeveType = "Short Sleeve" },
        new Sleeve { SleeveId = 2, SleeveType = "Long Sleeve" },
        new Sleeve { SleeveId = 3, SleeveType = "Sleeveless" },
        new Sleeve { SleeveId = 4, SleeveType = "3/4 Sleeve" },
        new Sleeve { SleeveId = 5, SleeveType = "Half Sleeve" },
        new Sleeve { SleeveId = 6, SleeveType = "No Sleeve" }
    };

        public static List<Fit> GetFits() => new List<Fit>
    {
        new Fit { FitId = 1, FitName = "Slim Fit" },
        new Fit { FitId = 2, FitName = "Regular Fit" },
        new Fit { FitId = 3, FitName = "Loose Fit" },
        new Fit { FitId = 4, FitName = "Athletic Fit" }
    };

        public static List<Size> GetSizes() => new List<Size>
    {
        new Size { SizeId = 1, SizeName = "XS" },
        new Size { SizeId = 2, SizeName = "S" },
        new Size { SizeId = 3, SizeName = "M" },
        new Size { SizeId = 4, SizeName = "L" },
        new Size { SizeId = 5, SizeName = "XL" },
        new Size { SizeId = 6, SizeName = "XXL" }
    };

        public static List<NeckType> GetNeckTypes() => new List<NeckType>
    {
        new NeckType { NeckTypeId = 1, NeckTypeName = "Round Neck" },
        new NeckType { NeckTypeId = 2, NeckTypeName = "V-Neck" },
        new NeckType { NeckTypeId = 3, NeckTypeName = "Collared" },
        new NeckType { NeckTypeId = 4, NeckTypeName = "Turtleneck" },
        new NeckType { NeckTypeId = 5, NeckTypeName = "Scoop Neck" },
        new NeckType { NeckTypeId = 6, NeckTypeName = "Other" }
    };

        public static List<FabricCare> GetFabricCares() => new List<FabricCare>
    {
        new FabricCare { FabricCareId = 1, CareInstructions = "Machine Wash" },
        new FabricCare { FabricCareId = 2, CareInstructions = "Hand Wash" },
        new FabricCare { FabricCareId = 3, CareInstructions = "Dry Clean Only" },
        new FabricCare { FabricCareId = 4, CareInstructions = "Do Not Bleach" },
        new FabricCare { FabricCareId = 5, CareInstructions = "Tumble Dry Low" },
        new FabricCare { FabricCareId = 6, CareInstructions = "Other" }
    };

        public static List<Category> GetCategories() => new List<Category>
    {
        new Category { CategoryId = 1, Name = "Men's Clothing" },
        new Category { CategoryId = 2, Name = "Women's Clothing" },
        new Category { CategoryId = 3, Name = "Kids' Clothing" },
        new Category { CategoryId = 4, Name = "Accessories" },
        new Category { CategoryId = 5, Name = "Footwear" },
        new Category { CategoryId = 6, Name = "Outerwear" },
        new Category { CategoryId = 7, Name = "Activewear" },
        new Category { CategoryId = 8, Name = "Swimwear" },
        new Category { CategoryId = 9, Name = "Sleepwear" },
        new Category { CategoryId = 10, Name = "Underwear" },
        new Category { CategoryId = 11, Name = "Formal Wear" },
        new Category { CategoryId = 12, Name = "Casual Wear" },
        new Category { CategoryId = 13, Name = "Work Wear" },
        new Category { CategoryId = 14, Name = "Ethnic Wear" },
        new Category { CategoryId = 15, Name = "Sportswear" },
        new Category { CategoryId = 16, Name = "Maternity Wear" }
    };

        public static List<Fabric> GetFabrics() => new List<Fabric>
    {
        new Fabric { FabricId = 1, FabricName = "Cotton" },
        new Fabric { FabricId = 2, FabricName = "Polyester" },
        new Fabric { FabricId = 3, FabricName = "Wool" },
        new Fabric { FabricId = 4, FabricName = "Silk" },
        new Fabric { FabricId = 5, FabricName = "Linen" },
        new Fabric { FabricId = 6, FabricName = "Nylon" },
        new Fabric { FabricId = 7, FabricName = "Rayon" },
        new Fabric { FabricId = 8, FabricName = "Spandex" },
        new Fabric { FabricId = 9, FabricName = "Velvet" },
        new Fabric { FabricId = 10, FabricName = "Fleece" },
        new Fabric { FabricId = 11, FabricName = "Other" }
    };

        public static List<Color> GetColors() => new List<Color>
    {
        new Color { ColorId = 1, ColorName = "Red" },
        new Color { ColorId = 2, ColorName = "Green" },
        new Color { ColorId = 3, ColorName = "Blue" },
        new Color { ColorId = 4, ColorName = "Yellow" },
        new Color { ColorId = 5, ColorName = "Black" },
        new Color { ColorId = 6, ColorName = "White" },
        new Color { ColorId = 7, ColorName = "Gray" },
        new Color { ColorId = 8, ColorName = "Pink" },
        new Color { ColorId = 9, ColorName = "Purple" },
        new Color { ColorId = 10, ColorName = "Orange" },
        new Color { ColorId = 11, ColorName = "Brown" },
        new Color { ColorId = 12, ColorName = "Cyan" },
        new Color { ColorId = 13, ColorName = "Magenta" },
        new Color { ColorId = 14, ColorName = "Beige" },
        new Color { ColorId = 15, ColorName = "Turquoise" },
        new Color { ColorId = 16, ColorName = "Other" }
    };
    }

}

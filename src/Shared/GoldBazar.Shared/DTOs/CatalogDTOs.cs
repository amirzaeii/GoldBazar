namespace GoldBazar.Shared.DTOs;

public record ItemResult(int PageIndex, int PageSize, int Count, List<ItemDTO> Data);
public record ItemCategoryDTO(int Id, string Name, string? PhotoUrl);
public record MaterialDTO
{
    public MaterialDTO()
    {

    }
    public MaterialDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public record CityDTO
{
    public CityDTO() { }

    public CityDTO(int id, string name, int governorateId, string governorateName)
    {
        Id = id;
        Name = name;
        GovernorateId = governorateId;
        GovernorateName = governorateName;
    }

    public CityDTO(int id, string name)
    {
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GovernorateId { get; set; }
    public string GovernorateName { get; set; } = string.Empty;

}
public record GovernateDTO
{
    public GovernateDTO()
    {

    }
    public GovernateDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public record ManufactureDTO(int Id, string Name, int Manufacture, decimal KT, decimal Purity);
public class MetalDTO
{
    public MetalDTO()
    {
    }

    public MetalDTO(int id, string name, int materialId, string materialName)
    {
        Id = id;
        Name = name;
        MaterialId = materialId;
        MaterialName = materialName;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MaterialId { get; set; }
    public string MaterialName { get; set; } = string.Empty;
}
public record OccasionDTO
{
    public OccasionDTO()
    {

    }
    public OccasionDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public record StyleDTO
{
    public StyleDTO()
    {

    }
    public StyleDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public record ItemDTO
{
    public ItemDTO()
    {
    }
    public ItemDTO(int id, string caption, string description, decimal costPerGram, decimal weight, int size,
        int typeId, string typeName, int metalId, string metalName, decimal kt, decimal purity,
        int shopId, string shopName, string city,
        int materialId, string materialName,
        int occasionId, string occasionName,
        int styleId, string styleName,
        decimal discount, bool status,
        int quantity,
        string categoryName,
        int categoryId,
        string mainPhoto)
    {
        Id = id;
        Caption = caption;
        Description = description;
        CostPerGram = costPerGram;
        Weight = weight;
        Size = size;
        TypeId = typeId;
        CategoryName = categoryName;
        CategoryId = categoryId;
        MetalId = metalId;
        MetalName = metalName;
        KT = kt;
        Purity = purity;
        ShopId = shopId;
        ShopName = shopName;
        City = city;
        MaterialId = materialId;
        MaterialName = materialName;
        OccasionId = occasionId;
        OccasionName = occasionName;
        StyleId = styleId;
        StyleName = styleName;
        Discount = discount;
        Status = status;
        Quantity = quantity;
        MainPhoto = mainPhoto;
    }

    public int Id { get; set; }
    public string Caption { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal CostPerGram { get; set; }
    public decimal ChangePriceRange { get; set; }
    public decimal Weight { get; set; }
    public int Size { get; set; }
    public int CategoryId { get; set; }
    public int ManufactureId { get; set; }
    public string ManufactureName { get; set; } = string.Empty;
    public int TypeId { get; set; } = 1;
    public string TypeName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int MetalId { get; set; } = 1;
    public string MetalName { get; set; } = string.Empty;
    public decimal KT { get; set; }
    public decimal Purity { get; set; }
    public int ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int MaterialId { get; set; } = 1;
    public string MaterialName { get; set; } = string.Empty;
    public int OccasionId { get; set; }
    public string OccasionName { get; set; } = string.Empty;
    public int StyleId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }
    public string StyleName { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public bool Status { get; set; }
    public int Quantity { get; set; } // Changed to settable
    public string MainPhoto { get; set; } = string.Empty;
}
public record ItemPhotosDTO
{
    public ItemPhotosDTO()
    {
    }

    public ItemPhotosDTO(int id, string absolutPath, string relativePath, string thumbnailPath, int priority, string description, int itemId)
    {
        Id = id;
        AbsolutePath = absolutPath;
        RelativePath = relativePath;
        ThumbnailPath = thumbnailPath;
        Priority = priority;
        Description = description;
        ItemId = itemId;
    }

    public int Id { get; set; }
    public string AbsolutePath { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public string ThumbnailPath { get; set; } = string.Empty;
    public int Priority { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ItemId { get; set; }

}
public class CompositeFilterDto
{
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public List<int> Metals { get; set; } = new();
    public List<int> Styles { get; set; } = new();
    public List<int> Occasions { get; set; } = new();
    public List<int> Materials { get; set; } = new();
    public List<int> ProductTypes { get; set; } = new();
}
namespace WebComponent.Dtos;

public record CatalogResult(int PageIndex, int PageSize, int Count, List<CatalogItem> Data);
public record CatalogItemType(int Id, string Name);
public record MaterialType(int Id, string Name);
public record MetalType(int Id, string Name, int manufacture, int kt, double purity);
public record OccasionType(int Id, string Name);
public record StyleType(int Id, string Name);

public class CatalogItem
{
    public CatalogItem()
    {
    }
    public CatalogItem(int id, string caption, string description, decimal costPerGram, decimal weight, int size,
        int typeId, string typeName, int metalId, string metalName, decimal kt, decimal purity,
        int shopId, string shopName, string city,
        int materialId, string materialName,
        int occasionId, string occasionName,
        int styleId, string styleName,
        decimal discount, bool status,
        int quantity,
        string mainPhoto)
    {
        Id = id;
        Caption = caption;
        Description = description;
        CostPerGram = costPerGram;
        Weight = weight;
        Size = size;
        TypeId = typeId;
        TypeName = typeName;
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

    public int Id { get; set;  }
    public string Caption { get; set;  }
    public string Description { get; set;  }
    public decimal CostPerGram { get; set;  }
    public decimal Weight { get; set;  }
    public int Size { get;set;   }
    public int TypeId { get; set;  }
    public string TypeName { get; set;  }
    public int MetalId { get;set;   }
    public string MetalName { get; set;  }
    public decimal KT { get; set;  }
    public decimal Purity { get;set;   }
    public int ShopId { get; set;  }
    public string ShopName { get; set;  }
    public string City { get; set;  }
    public int MaterialId { get; set;  }
    public string MaterialName { get; set;  }
    public int OccasionId { get; set;  }
    public string OccasionName { get;set;   }
    public int StyleId { get; set;  }
    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }
    public string StyleName { get; set;  }
    public decimal Discount { get; set;  }
    public bool Status { get;set;   }
    public int Quantity { get; set; } // Changed to settable
    public string MainPhoto { get; set;  }
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
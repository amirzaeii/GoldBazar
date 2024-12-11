

namespace Catalog.Infrastructure
{
    public record ProductDto
    {
        public int Id { get; }
        public string Caption { get;} = string.Empty;
        public bool ActivityStatus { get; }
        public bool HasOffer { get; }
        public decimal Discount { get;}
        public decimal CreateCost { get; set; }
        public double Weight { get; set; }
        public string ProductType { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Metal { get; set; } = string.Empty;
        public int Size { get; set; }
        public string Occasion { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CostPerGram { get; set; }
        public int Quantity { get; set; }
        public int ShopId { get; set; }
        public string MainImage { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;

        public ProductDto(Product product)
        {
            Id = product.Id;
            Caption = product.Caption;
            ActivityStatus = product.ActivityStatus;
            HasOffer = product.HasOffer;
            Discount = product.Discount;
            CreateCost = product.CreateCost;
            Weight = product.Weight;
            ProductType = product.ProductType;
            Material = product.Material;
            Metal = product.Metal;
            Size = product.Size > 15 ? 10 : 0;
            Occasion = product.Occasion;
            Style = product.Style;
            Description = product.Description;
            CostPerGram = product.CostPerGram;
            Quantity = product.Quantity;
            ShopId = product.ShopId;
            MainImage = product.MainImage;
            Manufacturer = product.Manufacturer;
        }

    }
}

namespace Catalog.Infrastructure
{
    public class Product
    {
        public int Id { get; set; }
        public string Caption { get; set; } = string.Empty;
        public bool ActivityStatus { get; set; }
        public bool HasOffer { get; set; }
        public decimal Discount { get; set; }
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
    }
}

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
        public ProductType Type { get; set; }
        public int TypeId { get; set; }
        public Material Material { get; set; } = default!;
        public int MaterialId { get; set; }
        public Metal Metal { get; set; } = default!;
        public int MetalId { get; set; } 
        public int Size { get; set; }
        public Occassion Occassion { get; set; } = default!;
        public int OccasionId { get; set; }
        public Style Style { get; set; } = default!;
        public int StyleId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal CostPerGram { get; set; }
        public int Quantity { get; set; }
        public Shop Shop { get; set; } = default!; 
        public int ShopId { get; set; }
        public string MainImage { get; set; } = string.Empty;
    }
}

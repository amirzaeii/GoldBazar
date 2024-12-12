using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Caption is required.")]
        [StringLength(100, ErrorMessage = "Caption cannot exceed 100 characters.")]
        public string Caption { get; set; } = string.Empty;

        public bool ActivityStatus { get; set; }
        public bool HasOffer { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0% and 100%.")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Create Cost is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Create Cost must be greater than 0.")]
        public decimal CreateCost { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Product Type is required.")]
        public int TypeId { get; set; } // FK to ProductType
        public ProductType Type { get; set; } = default!; // Navigation property

        [Required(ErrorMessage = "Material is required.")]
        public int MaterialId { get; set; } // FK to Material
        public Material Material { get; set; } = default!; // Navigation property

        [Required(ErrorMessage = "Metal is required.")]
        public string MetalId { get; set; } = string.Empty; // FK to Metal (alphanumeric key)
        public Metal Metal { get; set; } = default!; // Navigation property

        [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Occasion is required.")]
        public string OccasionId { get; set; } = string.Empty; // FK to Occasion
        public Occassion Occassion { get; set; } = default!; // Navigation property

        [Required(ErrorMessage = "Style is required.")]
        public string StyleId { get; set; } = string.Empty; // FK to Style
        public Style Style { get; set; } = default!; // Navigation property

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost Per Gram is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost Per Gram must be greater than 0.")]
        public decimal CostPerGram { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Shop is required.")]
        public int ShopId { get; set; } // FK to Shop
        public Shop Shop { get; set; } = default!; // Navigation property

        [Required(ErrorMessage = "Main Image is required.")]
        [Url(ErrorMessage = "Main Image must be a valid URL.")]
        public string MainImage { get; set; } = string.Empty;

        public String Manufacturer {  get; set; } 
    }
}

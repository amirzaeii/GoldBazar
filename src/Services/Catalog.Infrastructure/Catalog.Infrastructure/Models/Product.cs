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
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public double Weight { get; set; }

        public int TypeId { get; set; } // FK to ProductType
        public ProductType Type { get; set; } = default!; // Navigation property

        public int MaterialId { get; set; } // FK to Material
        public Material Material { get; set; } = default!; // Navigation property

        public int MetalId { get; set; } 
        public Metal Metal { get; set; } = default!; // Navigation property

        public int OccasionId { get; set; } // FK to Occasion
        public Occassion Occassion { get; set; } = default!; // Navigation property
        public string StyleId { get; set; } = string.Empty; // FK to Style
        public Style Style { get; set; } = default!; // Navigation property

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost Per Gram is required.")]
        public decimal CostPerGram { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }
        public int ShopId { get; set; } // FK to Shop
        public Shop Shop { get; set; } = default!; // Navigation property
    }
}

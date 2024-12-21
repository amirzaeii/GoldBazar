using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Infrastructure;

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
    public decimal Weight { get; set; }        
    public int TypeId { get; set; } // FK to ProductType
    [ForeignKey("TypeId")]
    public ProductType Type { get; set; } = default!; // Navigation property

    public int MaterialId { get; set; } // FK to Material
    [ForeignKey("MaterialId")]
    public Material Material { get; set; } = default!; // Navigation property

    public int MetalId { get; set; } 
    [ForeignKey("MetalId")]
    public Metal Metal { get; set; } = default!; // Navigation property

    public int OccasionId { get; set; } // FK to Occasion
    [ForeignKey("OccasionId")]
    public Occassion Occassion { get; set; } = default!; // Navigation property
    public int StyleId { get; set; } // FK to Style
    [ForeignKey("StyleId")]
    public Style Style { get; set; } = default!; // Navigation property

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cost Per Gram is required.")]
    public decimal CostPerGram { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
    public int Quantity { get; set; }
    public int ShopId { get; set; } // FK to Shop
     [ForeignKey("ShopId")]
    public Shop Shop { get; set; } = default!; // Navigation property

    // Added 
    [Range(0, int.MaxValue, ErrorMessage = "Size cannot be negative.")]
    public int Size { get; set; } // Optional or required, depending on business rules

}

namespace Catalog.Infrastructure.Models;

public class Shop
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public string Name { get; set; } = default!;
    public int CityId { get; set; } 

    [ForeignKey("CityId")]
    public City City { get; set; } = default!;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters.")]
    public string Address { get; set; } = default!;

    [Required(ErrorMessage = "Contact number is required.")]
    public string ContactNumber { get; set; } = default!;

    [StringLength(255, ErrorMessage = "Owner cannot exceed 255 characters.")]
    public string Owner { get; set; } = default!;
    public string Logo { get; set; } = default!;
    public string Banner { get; set; } = default!;
    public string? Description { get; set; }
    public int Status { get; set; } = default!;
    public string? Instagram { get; set; }
    public string? Tiktok { get; set; }
    public string? Snapchat { get; set; }
    public string? Facebook { get; set; }
}

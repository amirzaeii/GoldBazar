namespace Catalog.Infrastructure.Models;

public class Metal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Material is required.")]
    public int MaterialId { get; set; }   
    [ForeignKey("MaterialId")] 
    public Material Material { get; set; } = default!;
    public string? Image { get; set; }
}


namespace Catalog.Infrastructure.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = default!;
    public string? Photo { get; set; }
}

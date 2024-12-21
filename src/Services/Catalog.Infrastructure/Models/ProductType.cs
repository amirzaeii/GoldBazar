using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure;

public class ProductType
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = default!;
}

using System.ComponentModel.DataAnnotations;
namespace Catalog.Infrastructure.Models;

public class Occassion
{
    public int Id { get; set;  }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public string Name { get; set; } = default!;
}

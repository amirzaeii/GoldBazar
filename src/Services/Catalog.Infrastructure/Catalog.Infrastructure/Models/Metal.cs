using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure
{
    public class Metal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Manufacture is required.")]
        [RegularExpression("^(Dubai|Local|Turkey)$", ErrorMessage = "Manufacture must be one of the following: Dubai, Local, Turkey.")]
        public string? Manufacture { get; set; }

        [Required(ErrorMessage = "KT is required.")]
        [Range(18, 24, ErrorMessage = "KT must be a valid karat value (e.g., 18, 19, 20, ..., 24).")]
        public int KT { get; set; }
    }
}

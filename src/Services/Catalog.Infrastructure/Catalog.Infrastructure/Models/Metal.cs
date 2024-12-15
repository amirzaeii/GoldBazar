using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure
{
    public class Metal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Manufacture is required.")]
        public ManufactureEnum Manufacture { get; set; }

        [Required(ErrorMessage = "KT is required.")]
        [Range(1, 24, ErrorMessage = "KT must be a valid karat value (e.g., 18, 19, 20, ..., 24).")]
        public int Karat { get; set; }
    }

    public enum ManufactureEnum
    {
        Dubai = 1,
        Turkey = 2,
        Local = 3,
        Other = 4

    }
}

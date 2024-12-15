using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure
{
    public class Shop
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters.")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters.")]
        public string Address { get; set; } = default!;

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Contact number must be a valid international phone number.")]
        public string ContactNumber { get; set; } = default!;
        public string Owner { get; set; } = default!;
    }
}

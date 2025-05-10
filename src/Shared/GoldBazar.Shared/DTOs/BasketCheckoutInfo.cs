using System.ComponentModel.DataAnnotations;
namespace GoldBazar.Shared.DTOs;
public class BasketCheckoutInfo
{   
    [Required]
    public string? City { get; set; }
    [Required]
    public string? District { get; set; }
    [Required]
    public string? Street { get; set; }
    [Required]
    public string? Home { get; set; }

    [Required]
    public string? Tel { get; set; }

    [Required]
    public string? Location { get; set; }

    public string? Buyer { get; set; }
    public Guid RequestId { get; set; }
}

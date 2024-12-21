
namespace Catalog.Infrastructure;

public record ShopDto
{
    public ShopDto(Shop shop, decimal rate)
    {
        Id = shop.Id;
        Name = shop.Name;
        City = shop.City;
        Address = shop.Address;
        ContactNumber = shop.ContactNumber;
        Rating = rate;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public decimal Rating { get; set; }
}

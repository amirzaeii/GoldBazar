using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBazar.Shared.DTOs;
public class OrderDto
{
    public int Id { get; set; }
    public string? BuyerId { get; set; }
    public string? BuyerName { get; set; }
    public string? ShopName { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Submitted;
    public string? Description { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();

    public OrderDto() { }

    public OrderDto(string buyerId, string buyerName, int id, string shopName, Address address, int? orderId)
    {
        BuyerId = buyerId;
        BuyerName = buyerName;
        Id = id;
        ShopName = shopName;
        OrderDate = DateTime.UtcNow;
        Description = $"{address.Street}, {address.City}, {address.Province}";
    }

    public void AddOrderItem(int productId, string name, decimal price, decimal discount, string imageUrl, int quantity)
    {
        Items.Add(new OrderItemDto
        {
            ProductId = productId,
            Name = name,
            Price = price,
            Discount = discount,
            ImageUrl = imageUrl,
            Quantity = quantity
        });
    }

    public decimal GetTotal() => Items.Sum(i => (i.Price - i.Discount) * i.Quantity);
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class Address
{
    public string Type { get; }
    public string Street { get; }
    public string City { get; }
    public string Province { get; }
    public string Country { get; }
    public string PostalCode { get; }

    public Address(string type, string street, string city, string province, string country, string postalCode)
    {
        Type = type;
        Street = street;
        City = city;
        Province = province;
        Country = country;
        PostalCode = postalCode;
    }
}

public enum OrderStatus
{
    Submitted,
    AwaitingValidationFromVendor,
    AwaitingValidationFromGB,
    Shipping,
    Shipped,
    CancelledByBuyer,
    CancelledByVendor,
    CancelledByGB
}

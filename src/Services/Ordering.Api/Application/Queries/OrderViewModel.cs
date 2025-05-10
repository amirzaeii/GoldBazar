namespace Ordering.Api.Application.Queries;

public record Orderitem
{
    public string ProductName { get; init; }
    public int Units { get; init; }
    public double UnitPrice { get; init; }
    public string PictureUrl { get; init; }
    public int ProductId { get; init; }
}

public record Order
{
    public int OrderNumber { get; init; }
    public DateTime Date { get; init; }
    public int ShopId { get; init; }
    public string ShopName { get; set; }
    public string Status { get; init; }
    public string Description { get; init; }
    public string Street { get; init; }
    public string City { get; init; }
    public string District { get; init; }
    public string Tel { get; init; }
    public string Home { get; init; }
    public string Country { get; init; }
    public List<Orderitem> OrderItems { get; set; }
    public decimal Total { get; set; }
}

public record OrderSummary
{
    public int OrderNumber { get; init; }
    public DateTime Date { get; init; }
    public string Status { get; init; }
    public double Total { get; init; }
}

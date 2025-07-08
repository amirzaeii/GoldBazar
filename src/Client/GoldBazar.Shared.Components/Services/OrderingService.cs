using System.Net.Http.Json;

namespace GoldBazar.Shared.Components.Services;

public class OrderingService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "/api/Orders/";

    public Task<OrderDto[]> GetOrders()
    {
        return httpClient.GetFromJsonAsync<OrderDto[]>(remoteServiceBaseUrl)!;
    }
    public Task<OrderDto> GetOrders(int orderId)
    {
        return httpClient.GetFromJsonAsync<OrderDto>(remoteServiceBaseUrl + orderId)!;
    }

    public Task CreateOrder(CreateOrderRequest request, Guid requestId)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, remoteServiceBaseUrl);
        requestMessage.Headers.Add("x-requestid", requestId.ToString());
        requestMessage.Content = JsonContent.Create(request);
        return httpClient.SendAsync(requestMessage);
    }
}

public record OrderDto(
    string V,
    int OrderNumber,
    DateTime Date,
    string Status,
    decimal Total,
    OrderDetailDto[] OrderItems);

public record OrderDetailDto(
    int ProductId,
    string ProductName,
    int Units,
    decimal UnitPrice,
    string PictureUrl);
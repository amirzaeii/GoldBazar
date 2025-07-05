using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
// UNDER CONSTRUCTION
namespace GoldBazar.Shared.Components.Services;
public class OrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<OrderDto>> GetOrders()
        => await _http.GetFromJsonAsync<List<OrderDto>>("api/orders") ?? new();

    public async Task<OrderDto?> AddOrder(OrderDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/orders", dto);
        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }

    //public async Task<OrderDto?> UpdateOrder(OrderDto dto)
    //{
    //    var response = await _http.PutAsJsonAsync($"api/orders/{dto.Id}", dto);
    //    return await response.Content.ReadFromJsonAsync<OrderDto>();
    //}

    public async Task<bool> DeleteOrder(int id)
    {
        var response = await _http.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
}

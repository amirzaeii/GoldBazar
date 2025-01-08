
using WebComponent.Dtos;

namespace WebComponent.Services
{
    public interface IShopService
    {
        Task<Shop?> GetShopById(int id);
        Task<IEnumerable<Shop>> GetAllShops();
        Task<HttpResponseMessage> AddShop(Shop shop);
        Task<HttpResponseMessage> UpdateShop(int id, Shop updatedShop);
        Task<HttpResponseMessage> DeleteShop(int id);
    }
}

using WebComponent.Dtos;

namespace WebComponent.Services;

 public interface IBasketState
    {
        public Task<IReadOnlyCollection<BasketItem>> GetBasketItemsAsync();

        public Task AddAsync(CatalogItem item);
    }

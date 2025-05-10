namespace Catalog.Infrastructure.Events;
public record ConfirmedOrderStockItem(int ProductId, bool HasStock);

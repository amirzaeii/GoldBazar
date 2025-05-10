namespace Catalog.Infrastructure.Events;
public record OrderStatusChangedToAwaitingValidationIntegrationEvent(int OrderId, IEnumerable<OrderStockItem> OrderStockItems) : IntegrationEvent;

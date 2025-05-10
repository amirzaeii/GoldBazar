namespace Catalog.Infrastructure.Events;
public record OrderStockConfirmedIntegrationEvent(int OrderId) : IntegrationEvent;

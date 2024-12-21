namespace Catalog.Infrastructure;
public record OrderStockConfirmedIntegrationEvent(int OrderId) : IntegrationEvent;

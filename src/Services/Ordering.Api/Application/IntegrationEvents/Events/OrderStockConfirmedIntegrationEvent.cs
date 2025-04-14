namespace Ordering.Api.Application.IntegrationEvents.Events;

public record OrderStockConfirmedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public OrderStockConfirmedIntegrationEvent(int orderId) => OrderId = orderId;
}

namespace Ordering.Domain.Events;

/// <summary>
/// Event used when the order stock items are confirmed
/// </summary>
public class OrderStatusChangedToShippingDomainEvent
    : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToShippingDomainEvent(int orderId)
        => OrderId = orderId;
}

namespace Ordering.Domain.Events;

public class OrderCancelledByVendorDomainEvent : INotification
{
    public Order Order { get; }

    public OrderCancelledByVendorDomainEvent(Order order)
    {
        Order = order;
    }
}

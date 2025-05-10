namespace Ordering.Domain.Events;

public class OrderCancelledByBuyerDomainEvent : INotification
{
    public Order Order { get; }

    public OrderCancelledByBuyerDomainEvent(Order order)
    {
        Order = order;
    }
}


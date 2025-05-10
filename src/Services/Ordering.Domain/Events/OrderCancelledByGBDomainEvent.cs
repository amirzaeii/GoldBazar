namespace Ordering.Domain.Events;
public class OrderCancelledByGBDomainEvent : INotification
{
    public Order Order { get; }

    public OrderCancelledByGBDomainEvent(Order order)
    {
        Order = order;
    }
}
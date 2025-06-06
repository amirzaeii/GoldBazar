﻿namespace Ordering.Domain.Events;

/// <summary>
/// Event used when the grace period order is confirmed
/// </summary>
public class OrderStatusChangedToAwaitingValidationFromVendorDomainEvent
        : INotification
{
    public int OrderId { get; }
    public IEnumerable<OrderItem> OrderItems { get; }

    public OrderStatusChangedToAwaitingValidationFromVendorDomainEvent(int orderId,
        IEnumerable<OrderItem> orderItems)
    {
        OrderId = orderId;
        OrderItems = orderItems;
    }
}

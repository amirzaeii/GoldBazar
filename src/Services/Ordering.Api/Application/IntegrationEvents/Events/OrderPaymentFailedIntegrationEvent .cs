﻿namespace Ordering.Api.Application.IntegrationEvents.Events;

public record OrderPaymentFailedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public OrderPaymentFailedIntegrationEvent(int orderId) => OrderId = orderId;
}

namespace Ordering.Api.Application.IntegrationEvents.EventHandling;
public class OrderStatusChangedToShippingIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToShippingIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderStatusChangedToShippingIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToShippingIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new SetShippingOrderStatusCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}

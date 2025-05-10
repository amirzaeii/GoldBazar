namespace Ordering.Api.Application.IntegrationEvents.EventHandling;

public class OrderStatusChangedToCancelledByBuyerIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToCancelledByBuyerIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToCancelledByBuyerIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToCancelledByBuyerIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new CancelOrderByBuyerCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}
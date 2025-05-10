namespace Ordering.Api.Application.IntegrationEvents.EventHandling;

public class OrderStatusChangedToCancelledByGBIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToCancelledByGBIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToCancelledByGBIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToCancelledByGBIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new CancelOrderByGBCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}
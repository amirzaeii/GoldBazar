namespace Ordering.Api.Application.IntegrationEvents.EventHandling;

public class OrderStatusChangedToAwaitingValidationFromGBIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToAwaitingValidationFromGBIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationFromGBIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToAwaitingValidationFromGBIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new SetAwaitingValidationOrderFromGBStatusCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}

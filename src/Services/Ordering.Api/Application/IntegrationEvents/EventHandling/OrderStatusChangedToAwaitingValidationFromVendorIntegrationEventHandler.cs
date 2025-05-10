namespace Ordering.Api.Application.IntegrationEvents.EventHandling;

public class OrderStatusChangedToAwaitingValidationFromVendorIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToAwaitingValidationFromVendorIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationFromVendorIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToAwaitingValidationFromVendorIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new SetAwaitingValidationOrderFromVendorStatusCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}
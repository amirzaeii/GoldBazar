namespace Ordering.Api.Application.IntegrationEvents.EventHandling;

public class OrderStatusChangedToCancelledByVendorIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToCancelledByVendorIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToCancelledByVendorIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToCancelledByVendorIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        var command = new CancelOrderByVendorCommand(@event.OrderId);

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderNumber),
            command.OrderNumber,
            command);

        await mediator.Send(command);
    }
}
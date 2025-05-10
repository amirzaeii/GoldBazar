namespace Ordering.Api.Application.Commands;

// Regular CommandHandler
public class CancelOrderByBuyerCommandHandler : IRequestHandler<CancelOrderByBuyerCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderByBuyerCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handler which processes the command when
    /// customer executes cancel order from app
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(CancelOrderByBuyerCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
        if (orderToUpdate == null)
        {
            return false;
        }

        orderToUpdate.SetCancelledByBuyerStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


// Use for Idempotency in Command process
public class CancelOrderByBuyerIdentifiedCommandHandler : IdentifiedCommandHandler<CancelOrderByBuyerCommand, bool>
{
    public CancelOrderByBuyerIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CancelOrderByBuyerCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true; // Ignore duplicate requests for processing order.
    }
}

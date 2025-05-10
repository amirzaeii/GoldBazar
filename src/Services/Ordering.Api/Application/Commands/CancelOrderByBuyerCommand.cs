namespace Ordering.Api.Application.Commands;

public record CancelOrderByBuyerCommand(int OrderNumber) : IRequest<bool>;
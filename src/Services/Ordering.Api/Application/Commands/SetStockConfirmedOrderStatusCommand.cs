namespace Ordering.Api.Application.Commands;

public record SetStockConfirmedOrderStatusCommand(int OrderNumber) : IRequest<bool>;

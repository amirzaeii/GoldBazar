namespace Ordering.Api.Application.Commands;

public record ShippedOrderCommand(int OrderNumber) : IRequest<bool>;

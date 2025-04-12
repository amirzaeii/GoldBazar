namespace Ordering.Api.Application.Commands;

public record ShipOrderCommand(int OrderNumber) : IRequest<bool>;

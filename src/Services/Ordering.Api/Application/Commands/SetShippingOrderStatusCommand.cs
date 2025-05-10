namespace Ordering.Api.Application.Commands;

public record SetShippingOrderStatusCommand(int OrderNumber) : IRequest<bool>;

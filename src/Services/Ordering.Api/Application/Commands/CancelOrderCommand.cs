namespace Ordering.Api.Application.Commands;

public record CancelOrderCommand(int OrderNumber) : IRequest<bool>;


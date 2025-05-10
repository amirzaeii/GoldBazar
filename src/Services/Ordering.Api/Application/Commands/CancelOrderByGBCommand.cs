namespace Ordering.Api.Application.Commands;

public record CancelOrderByGBCommand(int OrderNumber) : IRequest<bool>;

namespace Ordering.Api.Application.Commands;

public record SetPaidOrderStatusCommand(int OrderNumber) : IRequest<bool>;

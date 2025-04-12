namespace Ordering.Api.Application.Commands;

public record SetAwaitingValidationOrderStatusCommand(int OrderNumber) : IRequest<bool>;

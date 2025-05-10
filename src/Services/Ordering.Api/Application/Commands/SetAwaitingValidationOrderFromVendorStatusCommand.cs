namespace Ordering.Api.Application.Commands;

public record SetAwaitingValidationOrderFromVendorStatusCommand(int OrderNumber) : IRequest<bool>;

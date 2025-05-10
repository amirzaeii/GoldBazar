
namespace Ordering.Api.Application.Commands;

public record SetAwaitingValidationOrderFromGBStatusCommand(int OrderNumber) : IRequest<bool>;

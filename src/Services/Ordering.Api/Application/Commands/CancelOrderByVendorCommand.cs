namespace Ordering.Api.Application.Commands;

public record CancelOrderByVendorCommand(int OrderNumber) : IRequest<bool>;


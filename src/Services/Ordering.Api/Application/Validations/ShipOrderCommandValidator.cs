namespace Ordering.Api.Application.Validations;

public class ShippingOrderCommandValidator : AbstractValidator<SetShippingOrderStatusCommand>
{
    public ShippingOrderCommandValidator(ILogger<ShippingOrderCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}


public class ShippedOrderCommandValidator : AbstractValidator<ShippedOrderCommand>
{
    public ShippedOrderCommandValidator(ILogger<ShippedOrderCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

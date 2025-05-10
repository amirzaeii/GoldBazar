namespace Ordering.Api.Application.Validations;

public class CancelOrderByBuyerCommandValidator : AbstractValidator<CancelOrderByBuyerCommand>
{
    public CancelOrderByBuyerCommandValidator(ILogger<CancelOrderByBuyerCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

public class CancelOrderByVendorCommandValidator : AbstractValidator<CancelOrderByVendorCommand>
{
    public CancelOrderByVendorCommandValidator(ILogger<CancelOrderByVendorCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
public class CancelOrderByGBCommandValidator : AbstractValidator<CancelOrderByGBCommand>
{
    public CancelOrderByGBCommandValidator(ILogger<CancelOrderByGBCommandValidator> logger)
    {
        RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}

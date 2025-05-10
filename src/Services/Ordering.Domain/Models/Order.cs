using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Models;

public class Order
    : Entity, IAggregateRoot
{
    public DateTime OrderDate { get; private set; }

    // Address is a Value Object pattern example persisted as EF Core 2.0 owned entity
    [Required]
    public Address Address { get; private set; } = default!;

    public int? BuyerId { get; private set; }

    public Buyer Buyer { get; } = default!;

    public OrderStatus OrderStatus { get; private set; }
    
    public string? Description { get; private set; }

    public int ShopId { get; private set; }
    
    public string ShopName { get; set; }

    // Draft orders have this set to true. Currently we don't check anywhere the draft status of an Order, but we could do it if needed
#pragma warning disable CS0414 // The field 'Order._isDraft' is assigned but its value is never used
    private bool _isDraft;
#pragma warning restore CS0414

    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method OrderAggregateRoot.AddOrderItem() which includes behavior.
    private readonly List<OrderItem> _orderItems;
   
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public int? PaymentId { get; private set; }

    public static Order NewDraft()
    {
        var order = new Order
        {
            _isDraft = true
        };
        return order;
    }

    protected Order()
    {
        _orderItems = [];
        _isDraft = false;
    }

    public Order(string userId, string userName, int shopId, string shopName, Address address, int? buyerId = null) : this()
    {
        BuyerId = buyerId;
        OrderStatus = OrderStatus.Submitted;
        OrderDate = DateTime.UtcNow;
        Address = address;
        ShopId = shopId;
        ShopName = shopName;

        // Add the OrderStarterDomainEvent to the domain events collection 
        // to be raised/dispatched when committing changes into the Database [ After DbContext.SaveChanges() ]
        AddOrderStartedDomainEvent(userId, userName);
    }

    // DDD Patterns comment
    // This Order AggregateRoot's method "AddOrderItem()" should be the only way to add Items to the Order,
    // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
    // in order to maintain consistency between the whole Aggregate. 
    public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
    {
        var existingOrderForProduct = _orderItems.SingleOrDefault(o => o.ProductId == productId);

        if (existingOrderForProduct != null)
        {
            //if previous line exist modify it with higher discount  and units..
            if (discount > existingOrderForProduct.Discount)
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }

            existingOrderForProduct.AddUnits(units);
        }
        else
        {
            //add validated new order item
            var orderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);
            _orderItems.Add(orderItem);
        }
    }

    public void SetAwaitingValidationFromVendorStatus()
    {
        if (OrderStatus == OrderStatus.Submitted)
        {
            AddDomainEvent(new OrderStatusChangedToAwaitingValidationFromVendorDomainEvent(Id, _orderItems));
            OrderStatus = OrderStatus.AwaitingValidationFromVendor;
            Description = "Order is awaiting validation from vendor.";
        }
    }

    public void SetAwaitingValidationFromGBtatus()
    {
        if (OrderStatus == OrderStatus.AwaitingValidationFromVendor)
        {
            AddDomainEvent(new OrderStatusChangedToAwaitingValidateFromGBDomainEvent(Id, _orderItems));

            OrderStatus = OrderStatus.AwaitingValidationFromGB;
            Description = "Order confrimed from Vendor and now it's awaiting validation from GoldBazar.";
        }
    }

    public void SetShippingStatus()
    {
        if (OrderStatus == OrderStatus.AwaitingValidationFromGB)
        {
            AddDomainEvent(new OrderStatusChangedToShippingDomainEvent(Id));

            OrderStatus = OrderStatus.Shipping;
            Description = "Order confrimed from GoldBazar and now it's being shipped.";
        }
    }

    public void SetShippedStatus()
    {
        if (OrderStatus != OrderStatus.Shipping)
        {
            StatusChangeException(OrderStatus.Shipped);
        }

        OrderStatus = OrderStatus.Shipped;
        Description = "The order was shipped.";
        AddDomainEvent(new OrderShippedDomainEvent(this));
    }

    public void SetCancelledByBuyerStatus()
    {
        if (OrderStatus == OrderStatus.Submitted ||
            OrderStatus == OrderStatus.AwaitingValidationFromVendor ||
            OrderStatus == OrderStatus.AwaitingValidationFromGB)
        {
            StatusChangeException(OrderStatus.CancelledByBuyer);
        }

        OrderStatus = OrderStatus.CancelledByBuyer;
        Description = "The order was cancelled by buyer.";
        AddDomainEvent(new OrderCancelledByBuyerDomainEvent(this));
    }
    public void SetCancelledByVendorStatus()
    {
        if (OrderStatus == OrderStatus.Submitted ||
            OrderStatus == OrderStatus.AwaitingValidationFromVendor)
        {
            StatusChangeException(OrderStatus.CancelledByVendor);
        }

        OrderStatus = OrderStatus.CancelledByVendor;
        Description = "The order was cancelled by vendor.";
        AddDomainEvent(new OrderCancelledByVendorDomainEvent(this));
    }
    public void SetCancelledByGBStatus()
    {
        if (OrderStatus == OrderStatus.Submitted ||
            OrderStatus == OrderStatus.AwaitingValidationFromGB ||
            OrderStatus == OrderStatus.Shipping ||
            OrderStatus == OrderStatus.Shipped)
        {
            StatusChangeException(OrderStatus.CancelledByGB);
        }

        OrderStatus = OrderStatus.CancelledByGB;
        Description = "The order was cancelled by GoldBazar.";
        AddDomainEvent(new OrderCancelledByGBDomainEvent(this));
    }

    private void AddOrderStartedDomainEvent(string userId, string userName)
    {
        var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName);

        AddDomainEvent(orderStartedDomainEvent);
    }

    private void StatusChangeException(OrderStatus orderStatusToChange)
    {
        throw new OrderingDomainException($"Is not possible to change the order status from {OrderStatus} to {orderStatusToChange}.");
    }

    public decimal GetTotal() => _orderItems.Sum(o => o.Units * o.UnitPrice);
}

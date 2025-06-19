using GoldBazar.Shared.DTOs;

using Microsoft.JSInterop;

using MudBlazor;
namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Order
{
    private List<OrderDto> orders = new();
    private bool _loading = true;

    protected override Task OnInitializedAsync()
    {
        _loading = true;

        var o1 = new OrderDto("user1", "Alice", 1, "Suli Store",
            new Address("Home", "123 Elm St", "Erbil", "Erbil Province", "Iraq", "44001"), 201);
        o1.AddOrderItem(1, "Gold Necklace", 150m, 10m, string.Empty, 1);
        o1.AddOrderItem(2, "Silver Ring", 50m, 0m, string.Empty, 2);
        o1.OrderStatus = OrderStatus.Submitted;

        var o2 = new OrderDto("user2", "Bob", 2, "Suli Store",
            new Address("Home", "456 Oak Ave", "Sulaymaniyah", "Sulaymaniyah Province", "Iraq", "46002"), null);
        o2.AddOrderItem(3, "Diamond Earrings", 200m, 20m, string.Empty, 1);
        o2.OrderStatus = OrderStatus.Shipping;

        var o3 = new OrderDto("user3", "Carol", 3, "Suli Store",
            new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
        o3.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
        o3.OrderStatus = OrderStatus.Shipped;

        var o4 = new OrderDto("user3", "Carol", 3, "Suli Store",
            new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
        o4.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
        o4.OrderStatus = OrderStatus.Shipped;

        var o5 = new OrderDto("user4", "Dave", 4, "Suli Store",
            new Address("Home", "101 Maple St", "Kirkuk", "Kirkuk Province", "Iraq", "36004"), 404);
        o5.AddOrderItem(5, "Ruby Pendant", 120m, 5m, string.Empty, 1);
        o5.OrderStatus = OrderStatus.CancelledByBuyer;

        var o6 = new OrderDto("user1", "Alice", 1, "Suli Store",
            new Address("Home", "123 Elm St", "Erbil", "Erbil Province", "Iraq", "44001"), 201);
        o6.AddOrderItem(1, "Gold Necklace", 150m, 10m, string.Empty, 1);
        o6.AddOrderItem(2, "Silver Ring", 50m, 0m, string.Empty, 2);
        o6.OrderStatus = OrderStatus.Submitted;

        var o7 = new OrderDto("user2", "Bob", 2, "Suli Store",
            new Address("Home", "456 Oak Ave", "Sulaymaniyah", "Sulaymaniyah Province", "Iraq", "46002"), null);
        o7.AddOrderItem(3, "Diamond Earrings", 200m, 20m, string.Empty, 1);
        o7.OrderStatus = OrderStatus.Shipping;

        var o8 = new OrderDto("user3", "Carol", 3, "Gamma Shop",
            new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
        o8.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
        o8.OrderStatus = OrderStatus.Shipped;

        orders = new() { o1, o2, o3, o4, o5, o6, o7, o8 };
        _loading = false;

        return Task.CompletedTask;
    }

    private void ViewOrderDetails(OrderDto order)
    {
        var index = orders.IndexOf(order) + 1;
        Snackbar.Add($"Viewing order #{index} at {order.ShopName}", Severity.Info);
    }

    private Color GetStatusColor(OrderStatus status) => status switch
    {
        OrderStatus.Submitted => Color.Primary,
        OrderStatus.AwaitingValidationFromVendor => Color.Warning,
        OrderStatus.AwaitingValidationFromGB => Color.Info,
        OrderStatus.Shipping => Color.Secondary,
        OrderStatus.Shipped => Color.Success,
        OrderStatus.CancelledByBuyer => Color.Error,
        OrderStatus.CancelledByVendor => Color.Error,
        OrderStatus.CancelledByGB => Color.Error,
        _ => Color.Default
    };

    private async Task ExportPdf()
    {
        await JSRuntime.InvokeVoidAsync("exportToPdfById", "ordersContainer", "orders_snapshot.pdf");
    }

    private List<OrderDto> GetSampleOrders() => new();
}

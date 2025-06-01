//using Microsoft.JSInterop;
//using MudBlazor;
//using Ordering.Domain.Models;
//using OrderModel = Ordering.Domain.Models.Order;
//namespace GoldBazar.Admin.Web.Components.Pages;

//public partial class Order
//{
//    private List<OrderModel> orders = new();
//    private bool _loading = true;

//    protected override Task OnInitializedAsync()
//    {
        
//        orders = new List<OrderModel>();

        
//        var o1 = new OrderModel("user1", "Alice", 1, "Suli Store", new Address("Home", "123 Elm St", "Erbil", "Erbil Province", "Iraq", "44001"), 201);
//        o1.AddOrderItem(1, "Gold Necklace", 150m, 10m, string.Empty, 1);
//        o1.AddOrderItem(2, "Silver Ring", 50m, 0m, string.Empty, 2);

        
//        var o2 = new OrderModel("user2", "Bob", 2, "Suli Store", new Address("Home", "456 Oak Ave", "Sulaymaniyah", "Sulaymaniyah Province", "Iraq", "46002"), null);
//        o2.AddOrderItem(3, "Diamond Earrings", 200m, 20m, string.Empty, 1);
//        o2.SetAwaitingValidationFromVendorStatus();
//        o2.SetAwaitingValidationFromGBtatus();
//        o2.SetShippingStatus();

        
//        var o3 = new OrderModel("user3", "Carol", 3, "Suli Store", new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
//        o3.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
//        o3.SetAwaitingValidationFromVendorStatus();
//        o3.SetAwaitingValidationFromGBtatus();
//        o3.SetShippingStatus();
//        o3.SetShippedStatus();


//        var o4 = new OrderModel("user3", "Carol", 3, "Suli Store", new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
//        o4.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
//        o4.SetAwaitingValidationFromVendorStatus();
//        o4.SetAwaitingValidationFromGBtatus();
//        o4.SetShippingStatus();
//        o4.SetShippedStatus();

//        var o5 = new OrderModel("user4", "Dave", 4, "Suli Store", new Address("Home", "101 Maple St", "Kirkuk", "Kirkuk Province", "Iraq", "36004"), 404);
//        o5.AddOrderItem(5, "Ruby Pendant", 120m, 5m, string.Empty, 1);
//        o5.SetAwaitingValidationFromVendorStatus();
//        o5.SetAwaitingValidationFromGBtatus();
//        o5.SetShippingStatus();
//        o5.SetCancelledByBuyerStatus();

//        var o6 = new OrderModel("user1", "Alice", 1, "Suli Store", new Address("Home", "123 Elm St", "Erbil", "Erbil Province", "Iraq", "44001"), 201);
//        o6.AddOrderItem(1, "Gold Necklace", 150m, 10m, string.Empty, 1);
//        o6.AddOrderItem(2, "Silver Ring", 50m, 0m, string.Empty, 2);


//        var o7 = new OrderModel("user2", "Bob", 2, "Suli Store", new Address("Home", "456 Oak Ave", "Sulaymaniyah", "Sulaymaniyah Province", "Iraq", "46002"), null);
//        o7.AddOrderItem(3, "Diamond Earrings", 200m, 20m, string.Empty, 1);
//        o7.SetAwaitingValidationFromVendorStatus();
//        o7.SetAwaitingValidationFromGBtatus();
//        o7.SetShippingStatus();


//        var o8 = new OrderModel("user3", "Carol", 3, "Gamma Shop", new Address("Home", "789 Pine Rd", "Duhok", "Duhok Province", "Iraq", "47003"), 303);
//        o8.AddOrderItem(4, "Platinum Bracelet", 300m, 50m, string.Empty, 1);
//        o8.SetAwaitingValidationFromVendorStatus();
//        o8.SetAwaitingValidationFromGBtatus();
//        o8.SetShippingStatus();
//        o8.SetShippedStatus();

//        orders.AddRange(new[] { o1, o2, o3, o4, o5, o6, o7 ,o8});
//        _loading = false;
//        return Task.CompletedTask;
//    }

//    private void ViewOrderDetails(OrderModel order)
//    {
//        var index = orders.IndexOf(order) + 1;
//        Snackbar.Add($"Viewing order #{index} at {order.ShopName}", Severity.Info);
//    }

//    private Color GetStatusColor(OrderStatus status) => status switch
//    {
//        OrderStatus.Submitted => Color.Primary,
//        OrderStatus.AwaitingValidationFromVendor => Color.Warning,
//        OrderStatus.AwaitingValidationFromGB => Color.Info,
//        OrderStatus.Shipping => Color.Secondary,
//        OrderStatus.Shipped => Color.Success,
//        OrderStatus.CancelledByBuyer => Color.Error,
//        OrderStatus.CancelledByVendor => Color.Error,
//        OrderStatus.CancelledByGB => Color.Error,
//        _ => Color.Default
//    };

//    private async Task ExportPdf()
//    {
//        await JSRuntime.InvokeVoidAsync("exportToPdfById", "ordersContainer", "orders_snapshot.pdf");
//    }

//    private List<OrderModel> GetSampleOrders()
//    {
//        var list = new List<OrderModel>();
//        return list;
//    }
//}

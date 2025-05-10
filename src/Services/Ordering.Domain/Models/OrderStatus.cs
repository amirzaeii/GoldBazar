using System.Text.Json.Serialization;

namespace Ordering.Domain.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Submitted = 1,
    AwaitingValidationFromVendor = 2,
    AwaitingValidationFromGB = 3,
    Shipping = 4,
    Shipped = 5,
    CancelledByVendor = 6,
    CancelledByGB = 7,
    CancelledByBuyer = 8
}

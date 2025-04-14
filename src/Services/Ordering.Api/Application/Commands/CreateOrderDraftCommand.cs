namespace Ordering.Api.Application.Commands;
using Ordering.Api.Application.Models;

public record CreateOrderDraftCommand(string BuyerId, IEnumerable<BasketItem> Items) : IRequest<OrderDraftDTO>;

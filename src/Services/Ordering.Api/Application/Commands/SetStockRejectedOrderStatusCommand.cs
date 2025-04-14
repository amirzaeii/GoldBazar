﻿namespace Ordering.Api.Application.Commands;

public record SetStockRejectedOrderStatusCommand(int OrderNumber, List<int> OrderStockItems) : IRequest<bool>;

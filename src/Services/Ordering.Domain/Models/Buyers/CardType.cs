﻿namespace Ordering.Domain.Models.Buyers;

public sealed class CardType
{
    public int Id { get; init; }
    public required string Name { get; init; }
}

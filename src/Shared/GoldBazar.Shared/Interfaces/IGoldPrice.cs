using System;

namespace GoldBazar.Shared.Interfaces;

public interface IGoldPrice
{
    decimal GetLivePrice();
    decimal CalculatePrice(decimal weight, decimal cost, int karat, decimal transferCost);
}

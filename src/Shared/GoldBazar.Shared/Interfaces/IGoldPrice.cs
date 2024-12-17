using GoldBazar.Shared.DTOs;
using System;

namespace GoldBazar.Shared.Interfaces;

public interface IGoldPrice
{
    Task<HaremAltinResponse> GetLivePricesAsync();
    decimal CalculatePrice(decimal weight, decimal cost, int karat, decimal transferCost);
}

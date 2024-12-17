﻿using System.Text;
using GoldBazar.Shared.DTOs;
using GoldBazar.Shared.Interfaces;
using Newtonsoft.Json;

namespace LivePrice
{
    public class HaremAltin 
    {
        private readonly HttpClient _httpClient;

        public HaremAltin(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HaremAltin() : this(new HttpClient())
        {
        }

        private const string ApiUrl = "https://www.haremaltin.com/dashboard/ajax/doviz";
        private const string Referrer = "https://www.haremaltin.com/canli-piyasalar/";

        // Function to fetch live ONS and USDKG prices
        public async Task<HaremAltinResponse> GetLivePricesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("x-requested-with", "XMLHttpRequest");
            request.Headers.Referrer = new Uri(Referrer);

            var content = new StringContent("dil_kodu=tr", Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Content = content;

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonConvert.DeserializeObject<RootObject>(responseBody);
                decimal onsPrice = jsonResponse.data.ONS.satis; // Use 'satis' for selling price
                decimal usdKgPrice = jsonResponse.data.USDKG.satis;

                return new HaremAltinResponse(onsPrice, usdKgPrice);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching live prices: " + ex.Message);
            }
        }

        // Function to calculate gold price
        public async Task<decimal> CalculateGoldPriceAsync(decimal goldWeight, int karate, ManufactureEnum manufacture, decimal createCost, decimal transferCostPerDollar)
        {
            decimal onsPrice, usdKgPrice;

            try
            {
                // Fetch live prices asynchronously
                var livePrices = await GetLivePricesAsync();
                onsPrice = livePrices.satisOns;
                usdKgPrice = livePrices.satisUSD;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to fetch live prices: " + ex.Message);
            }

            decimal kgPrice = 0;

            // Convert prices based on the manufacturer
            switch (manufacture)
            {
                case ManufactureEnum.Dubai:
                    kgPrice = onsPrice * 32.154m;
                    break;
                case ManufactureEnum.Turkey:
                    kgPrice = usdKgPrice;
                    break;
                case ManufactureEnum.Local:
                    kgPrice = onsPrice * 32.154m;
                    break;
                default:
                    kgPrice = onsPrice * 32.154m;
                    break;
            }

            // Adjust price per kg based on karate
            decimal pricePerKg = karate switch
            {
                21 => kgPrice * 0.875m,
                18 => kgPrice * 0.750m,
                22 => kgPrice * 0.916m,
                _ => kgPrice // Default for 24k
            };

            // Step 1: Calculate X (base gold price)
            decimal basePrice = (goldWeight * pricePerKg / 1000) + (goldWeight * createCost);

            // Step 2: Calculate Y (transfer cost)
            decimal transferCost = transferCostPerDollar * basePrice;

            // Final price in USD
            decimal totalPrice = basePrice + transferCost;

            return totalPrice;
        }
    }

    // JSON Models for API Response
    public class CurrencyDetails
    {
        public string code { get; set; } = string.Empty;
        public decimal alis { get; set; }
        public decimal satis { get; set; }
        public string tarih { get; set; } = string.Empty;
    }

    public class CurrencyData
    {
        public CurrencyDetails ONS { get; set; }
        public CurrencyDetails USDKG { get; set; }
    }

    public class RootObject
    {
        public CurrencyData data { get; set; }
    }
}
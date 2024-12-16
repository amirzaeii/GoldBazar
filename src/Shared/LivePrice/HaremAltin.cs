using System.Text;
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
        public async Task<(decimal onsPrice, decimal usdKgPrice)> GetLivePricesAsync()
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

                return (onsPrice, usdKgPrice);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching live prices: " + ex.Message);
            }
        }

        // Function to calculate gold price
        public async Task<decimal> CalculateGoldPriceAsync(decimal goldWeight, string karate, string manufacturer, decimal createCost, decimal transferCostPerDollar)
        {
            decimal onsPrice, usdKgPrice;

            try
            {
                // Fetch live prices asynchronously
                var livePrices = await GetLivePricesAsync();
                onsPrice = livePrices.onsPrice;
                usdKgPrice = livePrices.usdKgPrice;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to fetch live prices: " + ex.Message);
            }

            decimal kgPrice;

            // Convert prices based on the manufacturer
            if (manufacturer == "Dubai")
            {
                kgPrice = onsPrice * 32.154m;
            }
            else if (manufacturer == "Turkey" || manufacturer == "Local")
            {
                kgPrice = usdKgPrice; // Assuming Turkey's price is in USD per kg
            }
            else
            {
                throw new ArgumentException("Invalid manufacturer.");
            }

            // Adjust price per kg based on karate
            decimal pricePerKg = karate switch
            {
                "21" => kgPrice * 0.875m,
                "18" => kgPrice * 0.750m,
                "22" => kgPrice * 0.916m,
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

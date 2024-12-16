using System.Net;
using System.Text;
using Moq;
using Moq.Protected;
using Xunit;
using Assert = Xunit.Assert;
using LivePrice;

namespace LivePrice.Test
{
    public class HaremAltinTests
    {
        [Fact]
       public async Task GetLivePricesAsync_ShouldReturnPrices_WhenApiResponseIsValid()
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""data"": {""ONS"": {""satis"": 1950.5}, ""USDKG"": {""satis"": 62700}}}", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);

            var haremAltin = new HaremAltin(httpClient);

            // Act
            var result = await haremAltin.GetLivePricesAsync();

            // Assert
            Assert.Equal(1950.5m, result.onsPrice);
            Assert.Equal(62700m, result.usdKgPrice);
        }

        [Fact]
        public async Task GetLivePricesAsync_ShouldThrowException_WhenApiResponseIsInvalid()
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Invalid request")
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);

            var haremAltin = new HaremAltin(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await haremAltin.GetLivePricesAsync());
        }

        [Theory]
        [MemberData(nameof(GoldPriceTestData))]
        public async Task CalculateGoldPriceAsync_ShouldReturnCorrectPrice(decimal goldWeight, string karate, string manufacturer, decimal createCost, decimal transferCostPerDollar, decimal mockOnsPrice, decimal mockUsdKgPrice)
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent($"{{\"data\": {{\"ONS\": {{\"satis\": {mockOnsPrice}}}, \"USDKG\": {{\"satis\": {mockUsdKgPrice}}}}}}}")
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);

            var haremAltin = new HaremAltin(httpClient);

            // Act
            var price = await haremAltin.CalculateGoldPriceAsync(goldWeight, karate, manufacturer, createCost, transferCostPerDollar);

            // Assert
            Assert.True(price > 0, "The calculated price should be positive.");
        }

        [Fact]
        public async Task CalculateGoldPriceAsync_ShouldThrowException_WhenInvalidManufacturer()
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""data"": {""ONS"": {""satis"": 1950.5}, ""USDKG"": {""satis"": 62700}}}", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);

            var haremAltin = new HaremAltin(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await haremAltin.CalculateGoldPriceAsync(10, "21", "InvalidManufacturer", 5m, 0.02m));
        }

        [Fact]
        public async Task CalculateGoldPriceAsync_ShouldHandleZeroWeight()
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""data"": {""ONS"": {""satis"": 1950.5}, ""USDKG"": {""satis"": 62700}}}", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);

            var haremAltin = new HaremAltin(httpClient);

            // Act
            var price = await haremAltin.CalculateGoldPriceAsync(0, "21", "Dubai", 5m, 0.02m);

            // Assert
            Assert.Equal(0, price);
        }

        public static IEnumerable<object[]> GoldPriceTestData()
        {
            yield return new object[] { 10m, "21", "Dubai", 5m, 0.02m, 1950.5m, 62700m };
            yield return new object[] { 20m, "18", "Turkey", 10m, 0.01m, 1950.5m, 62700m };
            yield return new object[] { 50m, "22", "Local", 8m, 0.015m, 1950.5m, 62700m };
        }
    }
}

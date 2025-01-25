using System.Net; 
using System.Net.Http; 
using System.Threading.Tasks; 
using interview_integrationstask.Models; 
using interview_integrationstask.Services; 
using Microsoft.Extensions.Logging; 
using Microsoft.Extensions.Options; 
using Moq; 
using Moq.Protected; 
using Xunit; 

namespace interview_integrationstask.Tests.Services 
{
    public class FootballApiServiceTests 
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock; 
        private readonly Mock<ILogger<FootballApiService>> _loggerMock; 
        private readonly IOptions<FootballApiOptions> _options; 

        public FootballApiServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _loggerMock = new Mock<ILogger<FootballApiService>>();
            _options = Options.Create(new FootballApiOptions 
            {
                BaseUrl = "https://api.football-data.org/v4"
            });
        }

        private FootballApiService CreateService(HttpMessageHandler handler)
        {
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(_options.Value.BaseUrl)
            };

            _httpClientFactoryMock 
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(client);
            
            return new FootballApiService(_httpClientFactoryMock.Object, _options, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTeamsAsync_ReturnsTeams_WhenApiResponseIsValid()
        {
            // Arrange 
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock 
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage 
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"resultSet\": [{\"id\": 1, \"name\": \"Team A\"}]}")
                });
            
            var service = CreateService(handlerMock.Object);

            // Act 
            var result = await service.GetTeamsAsync();

            // Assert 
            Assert.NotNull(result);
            Assert.Single(result.ResultSet);
            Assert.Equal(1, result.ResultSet.First().Id);
            Assert.Equal("Team A", result.ResultSet.First().Name);
        }

        [Fact]
        public async Task GetTeamsAsync_ThrowsException_WhenApiReturnsNotFound()
        {
            // Arrange 
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock 
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });
            
            var service = CreateService(handlerMock.Object);

            // Act & Assert 
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetTeamsAsync());
        }
    }
}
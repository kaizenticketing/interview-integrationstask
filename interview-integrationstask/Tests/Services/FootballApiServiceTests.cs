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
        public async Task GetTeamsByIdAsync_ReturnsTeam_WhenApiResponseIsValid()
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
                    Content = new StringContent("{\"id\": 1, \"name\": \"Team A\"}")
                });
            
            var service = CreateService(handlerMock.Object);

            // Act 
            var result = await service.GetTeamsByIdAsync(1);

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Team A", result.Name);
        }

        [Fact]
        public async Task GetTeamsByIdAsync_ThrowsException_WhenApiReturnsNotFound()
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
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetTeamsByIdAsync(1));
        }

        [Fact]
        public async Task GetScoresAsync_ReturnsScores_WhenApiResponseIsValid()
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
                    Content = new StringContent("{\"matches\": [{\"id\": 101, \"status\": \"FINISHED\"}]}")
                });
            
            var service = CreateService(handlerMock.Object);

            // Act 
            var result = await service.GetScoresAsync(1, "2023-01-01", "2023-12-31");

            // Assert 
            Assert.NotNull(result);
            Assert.NotEmpty(result.Matches);
            Assert.Equal(101, result.Matches.First().Id);
            Assert.Equal("FINISHED", result.Matches.First().Status);
        }

        [Fact]
        public async Task GetScoresAsync_ThrowsException_WhenApiReturnsUnauthorized()
        {
            // Arrange 
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock 
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage {
                    StatusCode = HttpStatusCode.Unauthorized
                });
            
            var service = CreateService(handlerMock.Object);

            // Act & Assert 
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
                service.GetScoresAsync(1, "2023-01-01", "2023-12-31"));
        }

        [Fact]
        public async Task GetScoresAsync_ThrowsRateLimitRejectedException_WhenRateLimitIsExceeded()
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
                    StatusCode = HttpStatusCode.TooManyRequests
                });
            
            var service = CreateService(handlerMock.Object);

            // Act & Assert 
            await Assert.ThrowsAsync<RateLimitRejectedException>(() => 
                service.GetScoresAsync(1, "2023-01-01", "2023-12-31"));
        }

        [Fact]
        public async Task GetCompetitionAsync_ReturnsCompetition_WhenApiResponseIsValid()
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
                    Content = new StringContent("{\"id\": 2021, \"name\": \"Premier League\", \"code\": \"PL\"}")
                });
            
            var service = CreateService(handlerMock.Object);

            // Act 
            var result = await service.GetCompetitionAsync("PL");

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2021, result.Id);
            Assert.Equal("Premier League", result.Name);
            Assert.Equal("PL", result.Code);
        }

        [Fact]
        public async Task GetCompetitionAsync_ThrowsKeyNotFOundException_WhenApiReturnsNotFound()
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
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetCompetitionAsync("INVALID_CODE"));
        }

        [Fact]
        public async Task GetCompetitionAsync_ThrowsUnauthorizedAccessException_WhenApiReturnsUnauthorized()
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
                    StatusCode = HttpStatusCode.Unauthorized
                });
            
            var service = CreateService(handlerMock.Object);

            // Act & Assert 
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.GetCompetitionAsync("PL"));
        }
    }
}
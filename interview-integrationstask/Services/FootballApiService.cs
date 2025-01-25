using System.Net; 
using System.Text.Json; 
using interview_integrationstask.Models; 
using Microsoft.Extensions.Options; 
using Polly; 
using Polly.RateLimit;

namespace interview_integrationstask.Services 
{
    /// <summary>
    /// Implementation of football data API service using football-data.org
    /// </summary>
    public class FootballApiService: IFootballApiService 
    {
        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly FootballApiOptions _options; 
        private readonly ILogger<FootballApiService> _logger; 
        private readonly AsyncRateLimitPolicy<HttpResponseMessage> _rateLimitPolicy; 

        public FootballApiService(
            IHttpClientFactory httpClientFactory, 
            IOptions<FootballApiOptions> options, 
            ILogger<FootballApiService> logger)
        {
            _httpClientFactory = httpClientFactory; 
            _options = options.Value; 
            _logger = logger; 

            // COnfigure rate limiting policy (10 requests per minute)
            _rateLimitPolicy = Policy.RateLimitAsync<HttpResponseMessage>(10, TimeSpan.FromMinutes(1));
        }

        private async Task<T> SendRequestAsync<T>(string endpoint)
        {
            using var client = _httpClientFactory.CreateClient("FootballApi");

            try 
            {
                var response = await _rateLimitPolicy.ExecuteAsync(async () => 
                    await client.GetAsync($"{_options.BaseUrl}/{endpoint}"));
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content)
                        ?? throw new JsonException("Failed to deserialize response");
                }

                throw response.StatusCode switch 
                {
                    HttpStatusCode.NotFound => new KeyNotFoundException($"Resource not found: {endpoint}"),
                    HttpStatusCode.Unauthorized => new UnauthorizedAccessException("Invalid API key"),
                    HttpStatusCode.TooManyRequests => new RateLimitRejectedException("API rate limit exceeded"),
                    _ => new HttpRequestException($"API request failed with status code {response.StatusCode}")
                };
            }
            catch (RateLimitRejectedException)
            {
                _logger.LogWarning("Rate limit exceeded for football API");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<ApiPaginatedResponse<IEnumerable<Team>>> GetTeamsAsync()
        {
            _logger.LogInformation("Retrieving all teams");
            return await SendRequestAsync<ApiPaginatedResponse<IEnumerable<Team>>>("teams");
        }

        /// <inheritdoc />
        public async Task<Team> GetTeamsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Team cannot be empty", nameof(name));

            _logger.LogInformation("Retrieving team information for {TeamName}", name);
            return await SendRequestAsync<Team>($"teams/{Uri.EscapeDataString(name)}");
        }

        // <inheritdoc />
        public async Task<Competition> GetLeagueByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Leage name cannot be empty", nameof(name));

            _logger.LogInformation("Retrieving league information for {LeagueName}", name);
            return await SendRequestAsync<Competition>($"competitions/{Uri.EscapeDataString(name)}");
        }

        /// <inheritdoc/>
        public async Task<ApiPaginatedResponse<IEnumerable<Match>>> GetFixturesAsync()
        {
            _logger.LogInformation("Retrieving upcoming fixtures");
            return await SendRequestAsync<ApiPaginatedResponse<IEnumerable<Match>>>("matches?status=SCHEDULED");
        } 

        /// <inheritdoc/>
        public async Task<ApiPaginatedResponse<IEnumerable<Match>>> GetScoresAsync()
        {
            _logger.LogInformation("Retrieving latest scores");
            return await SendRequestAsync<ApiPaginatedResponse<IEnumerable<Match>>>("matches?status=FINISHED");
        }
    }

    /// <summary>
    /// Exception throw when API rate limit is exceeded
    /// </summary>
    public class RateLimitRejectedException : Exception 
    {
        public RateLimitRejectedException(string message) : base(message) { }
    }
}
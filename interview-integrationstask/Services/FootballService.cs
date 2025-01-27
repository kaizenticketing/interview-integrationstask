using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using interview_integrationstask.Interfaces.Services;
using interview_integrationstask.Models;
using Microsoft.Extensions.Options;

namespace interview_integrationstask.Services
{
    public class FootballService : IFootballService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly ILogger<FootballService> _logger;

        public FootballService(HttpClient httpClient, IOptions<FootballData> footballData, ILogger<FootballService> logger)
        {
            _httpClient = httpClient;
            _apiKey = footballData.Value.ApiKey;
            _apiUrl = footballData.Value.ApiUrl;
            _logger = logger;
        }

        private async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

                var response = await _httpClient.GetAsync($"{_apiUrl}{endpoint}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to fetch data: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(content);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Encountered an error during API Call: {ex.Message}", ex);
                throw;
            }
        }


        public async Task<TeamInfo> GetTeamById(int teamId, int? season = null)
        {
            var endpoint = $"teams/{teamId}";

            var queryParamaters = new List<string>();

            if (season.HasValue)
            {
                queryParamaters.Add($"season={season}");
            }

            if (queryParamaters.Any())
            {
                endpoint += "?" + string.Join("&", queryParamaters);
            }
            var data = await GetAsync<TeamInfo>(endpoint);
            return data;
        }

        public async Task<MatchInfo> GetRecentMatchesByTeam(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null)
        {
            var endpoint = $"teams/{teamId}/matches?status=FINISHED";

            if (limit == null || limit > 50)
            {
                limit = 50;
            }

            if (season == null)
            {
                if (dateFrom == null)
                {
                    dateFrom = DateTime.Now.Date.AddDays(-30);
                }

                if (dateTo == null)
                {
                    dateTo = DateTime.Now.Date;
                }
            }

            var queryParamaters = new List<string>();

            queryParamaters.Add($"dateFrom={dateFrom:yyyy-MM-dd}");
            queryParamaters.Add($"dateTo={dateTo:yyyy-MM-dd}");


            if (season.HasValue)
            {
                queryParamaters.Add($"season={season}");
            }

            if (!string.IsNullOrEmpty(venue))
            {
                queryParamaters.Add($"venue={venue}");
            }

            queryParamaters.Add($"limit={limit}");


            if (queryParamaters.Any())
            {
                endpoint += "&" + string.Join("&", queryParamaters);
            }
            var data = await GetAsync<MatchInfo>(endpoint);
            return data;
        }

        public async Task<MatchInfo> GetUpcomingFixturesByTeam(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null)
        {
            var endpoint = $"teams/{teamId}/matches?status=SCHEDULED";

            if (limit == null || limit > 50)
            {
                limit = 50;
            }

            if (season == null)
            {
                if (dateFrom == null)
                {
                    dateFrom = DateTime.Now.Date;
                }

                if (dateTo == null)
                {
                    dateTo = DateTime.Now.Date.AddDays(30);
                }
            }

            var queryParamaters = new List<string>();

            queryParamaters.Add($"dateFrom={dateFrom:yyyy-MM-dd}");

            queryParamaters.Add($"dateTo={dateTo:yyyy-MM-dd}");


            if (season.HasValue)
            {
                queryParamaters.Add($"season={season}");
            }

            if (!string.IsNullOrEmpty(venue))
            {
                queryParamaters.Add($"venue={venue}");
            }

            queryParamaters.Add($"limit={limit}");


            if (queryParamaters.Any())
            {
                endpoint += "&" + string.Join("&", queryParamaters);
            }
            var data = await GetAsync<MatchInfo>(endpoint);
            return data;
        }
    }
}

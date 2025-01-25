using System.Text.Json.Serialization; 

namespace interview_integrationstask.Models 
{
    /// <summary>
    /// Base class for paginated API responses 
    /// </summary>
    public record ApiPaginatedResponse<T>
    {
        [JsonPropertyName("count")]
        public int Count { get; init; }

        [JsonPropertyName("filters")]
        public Dictionary<string, string>? FIlters { get; init; }

        [JsonPropertyName("resultSet")]
        public T ResultSet { get; init; } = default!; 
    }

    /// <summary>
    /// Represents a football team 
    /// </summary>
    public record Team 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty; 

        [JsonPropertyName("shortName")]
        public string? ShortName { get; init; }

        [JsonPropertyName("tla")]
        public string? Tla { get; init; }

        [JsonPropertyName("crest")]
        public string? CrestUrl { get; init; }

        [JsonPropertyName("founded")]
        public int? Founded { get; init; }

        [JsonPropertyName("venue")]
        public string? Venue { get; init; }
    }

    /// <summary>
    /// Represents a football competition/league 
    /// </summary>
    public record Competition 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty; 

        [JsonPropertyName("code")]
        public string Code { get; init; } = string.Empty; 

        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty; 

        [JsonPropertyName("emblem")]
        public string? EmblemUrl { get; init; }

        [JsonPropertyName("currentSeason")]
        public Season CurrentSeason { get; init; }
    }

    /// <summary>
    /// Represents a season within a competition
    /// </summary>
    public record Season 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; init; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; init; }

        [JsonPropertyName("currentMatchday")]
        public int? CurrentMatchday { get; init; }
    }

    /// <summary>
    /// Represents a football match/fixture 
    /// </summary>
    public record Match 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("competition")]
        public Competition Competition { get; init; } = default!;

        [JsonPropertyName("homeTeam")]
        public Team HomeTeam { get; init; } = default!;

        [JsonPropertyName("awayTeam")]
        public Team AwayTeam { get; init; } = default!;

        [JsonPropertyName("utcDate")]
        public DateTime UtcDate { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; } = string.Empty; 
    }
}
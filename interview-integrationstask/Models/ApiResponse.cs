using System.Text.Json.Serialization; 

namespace interview_integrationstask.Models 
{

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

        [JsonPropertyName("runningCompetitions")]
        public IEnumerable<Competition> RunningCompetitions { get; init; } = new List<Competition>();
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

        [JsonPropertyName("area")]
        public Area? Area { get; init; }

        [JsonPropertyName("currentSeason")]
        public Season? CurrentSeason { get; init; }

        [JsonPropertyName("seasons")]
        public IEnumerable<Season>? Seasons { get; init; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; init; }
    }

    /// <summary>
    /// Represents a football match/fixture 
    /// </summary>
    public record Match
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("competition")]
        public Competition Competition { get; init; } = new Competition();

        [JsonPropertyName("homeTeam")]
        public Team HomeTeam { get; init; } = new Team();

        [JsonPropertyName("awayTeam")]
        public Team AwayTeam { get; init; } = new Team();

        [JsonPropertyName("utcDate")]
        public DateTime UtcDate { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; } = string.Empty; 

        [JsonPropertyName("score")]
        public Score Score { get; init; } = new Score(); 
    }

    /// <summary>
    /// Represents the result set metadata for a paginated API response.
    /// </summary>
    public record ResultSet 
    {
        [JsonPropertyName("count")]
        public int Count { get; init; }

        [JsonPropertyName("competitions")]
        public string Competitions { get; init; } = string.Empty; 

        [JsonPropertyName("first")]
        public string First { get; init; } = string.Empty; 

        [JsonPropertyName("last")]
        public string Last { get; init; } = string.Empty;

        [JsonPropertyName("played")]
        public int Played { get; init; }

        [JsonPropertyName("wins")]
        public int Wins { get; init; }

        [JsonPropertyName("draws")]
        public int Draws { get; init; }

        [JsonPropertyName("losses")]
        public int Losses { get; init; }
    }

    /// <summary>
    /// Paginated API response containing matches and metadata 
    /// </summary>
    public record MatchesApiResponse
    {
        [JsonPropertyName("filters")]
        public Dictionary<string, object>? Filters { get; init; }

        [JsonPropertyName("resultSet")]
        public ResultSet ResultSet { get; init; } = default!;

        [JsonPropertyName("matches")]
        public IEnumerable<Match> Matches { get; init; } = new List<Match>();
    }

    /// <summary>
    /// Represents the score details of a football match. 
    /// </summary>
    public record Score
    {
        [JsonPropertyName("winner")]
        public string Winner { get; init; } = string.Empty; 

        [JsonPropertyName("duration")]
        public string Duration { get; init; } = string.Empty; 

        [JsonPropertyName("fullTime")]
        public PeriodScore FullTime { get; init; } = new PeriodScore();

        [JsonPropertyName("halftime")]
        public PeriodScore HalfTime { get; init; } = new PeriodScore();
    }

    /// <summary>
    /// Represents the score for a specific period of a football match (e.g., full-time, half-time).
    /// </summary
    public record PeriodScore 
    {
        [JsonPropertyName("home")]
        public int Home { get; init; }

        [JsonPropertyName("away")]
        public int Away { get; init; }
    }

    public record Area 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty; 

        [JsonPropertyName("code")]
        public string? Code { get; init; }

        [JsonPropertyName("flag")]
        public string? Flag { get; init; }
    }

    public record Season 
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; init; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; init; }

        [JsonPropertyName("currentMatchDay")]
        public int? CurrentMatchday { get; init; }

        [JsonPropertyName("winner")]
        public Team? Winner { get; init; }

        [JsonPropertyName("stages")]
        public IEnumerable<string> Stages { get; init; } = new List<string>();
    }
}
using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class TeamInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("shortName")]
        public string? ShortName { get; set; }

        [JsonPropertyName("tla")]
        public string? Tla { get; set; }

        [JsonPropertyName("crest")]
        public string? CrestUrl { get; set; }

        [JsonPropertyName("founded")]
        public int? Founded { get; set; }

        [JsonPropertyName("venue")]
        public string? Venue { get; set; }

        [JsonPropertyName("runningCompetitions")]
        public List<Competition>? RunningCompetitions { get; set; } = new List<Competition>();

        [JsonPropertyName("area")]
        public Area? Area { get; set; }

        [JsonPropertyName("coach")]
        public Coach? Coach { get; set; }

        [JsonPropertyName("squad")]
        public List<Squad>? Squad { get; set; }

        [JsonPropertyName("staff")]
        public List<object>? Staff { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }

    }
}

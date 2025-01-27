using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class CompetitionInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("emblem")]
        public string? EmblemUrl { get; set; }

        [JsonPropertyName("currentSeason")]
        public Season? Season { get; set; }

        [JsonPropertyName("seasons")]
        public List<Season>? Seasons { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }
    }
}

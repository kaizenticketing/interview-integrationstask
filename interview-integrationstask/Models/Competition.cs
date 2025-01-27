using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Competition
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("emblem")]
        public string? EmblemUrl { get; set; }

        [JsonPropertyName("area")]
        public Area? Area { get; set; }

        [JsonPropertyName("currentSeason")]
        public Season? CurrentSeason { get; init; }

        [JsonPropertyName("seasons")]
        public List<Season>? Seasons { get; init; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; init; }
    }
}

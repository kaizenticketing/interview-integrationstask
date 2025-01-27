
using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class Score
    {
        [JsonPropertyName("winner")]
        public string? Winner { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        [JsonPropertyName("fullTime")]
        public FullTime? FullTime { get; set; }

        [JsonPropertyName("halfTime")]
        public HalfTime? HalfTime { get; set; }
    }
}

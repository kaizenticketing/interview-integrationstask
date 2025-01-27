using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class ResultSet
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("competitions")]
        public string Competitions { get; set; }

        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("played")]
        public int Played { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("draws")]
        public int Draws { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }
    }
}

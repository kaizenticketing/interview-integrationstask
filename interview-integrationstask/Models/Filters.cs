using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class Filters
    {
        [JsonPropertyName("competitions")]
        public string Competitions { get; set; }

        [JsonPropertyName("permission")]
        public string Permission { get; set; }

        [JsonPropertyName("status")]
        public List<string> Status { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }
    }
}

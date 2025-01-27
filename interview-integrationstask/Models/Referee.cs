using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class Referee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }
    }
}

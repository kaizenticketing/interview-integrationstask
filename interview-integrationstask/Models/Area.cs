using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Area
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }
    }
}

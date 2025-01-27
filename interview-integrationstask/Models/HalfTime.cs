
using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class HalfTime
    {
        [JsonPropertyName("home")]
        public int? Home { get; set; }

        [JsonPropertyName("away")]
        public int? Away { get; set; }
    }
}
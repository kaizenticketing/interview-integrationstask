using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class Odds
    {
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
    }
}

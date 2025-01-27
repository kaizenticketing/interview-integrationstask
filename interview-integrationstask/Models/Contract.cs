using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Contract
    {
        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("until")]
        public string Until { get; set; }
    }
}

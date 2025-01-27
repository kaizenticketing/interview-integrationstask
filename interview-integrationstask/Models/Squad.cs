using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Squad
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }
    }
}

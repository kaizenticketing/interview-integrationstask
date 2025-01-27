using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Coach
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("contract")]
        public Contract Contract { get; set; }
    }
}

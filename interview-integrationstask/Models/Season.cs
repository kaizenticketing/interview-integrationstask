using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace interview_integrationstask.Models
{
    public class Season
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("currentMatchday")]
        public int? CurrentMatchday { get; set; }

        [JsonPropertyName("winner")]
        public TeamInfo? Winner { get; set; }

        [JsonPropertyName("stages")]
        public List<string> Stages { get; set; } = new List<string>();
    }
}

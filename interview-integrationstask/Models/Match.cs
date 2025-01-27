using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class Match
    {
        [JsonPropertyName("area")]
        public Area Area { get; set; }

        [JsonPropertyName("competition")]
        public Competition Competition { get; set; }

        [JsonPropertyName("season")]
        public Season Season { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("utcDate")]
        public DateTime UtcDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("matchday")]
        public int Matchday { get; set; }

        [JsonPropertyName("stage")]
        public string Stage { get; set; }

        [JsonPropertyName("group")]
        public object Group { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("homeTeam")]
        public TeamInfo HomeTeam { get; set; }

        [JsonPropertyName("awayTeam")]
        public TeamInfo AwayTeam { get; set; }

        [JsonPropertyName("score")]
        public Score? Score { get; set; }

        [JsonPropertyName("odds")]
        public Odds? Odds { get; set; }

        [JsonPropertyName("referees")]
        public List<Referee>? Referees { get; set; }
    }
}

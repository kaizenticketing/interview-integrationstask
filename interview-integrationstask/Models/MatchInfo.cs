using System.Text.Json.Serialization;

namespace interview_integrationstask.Models
{
    public class MatchInfo
    {
        [JsonPropertyName("filters")]
        public Filters Filters { get; set; }

        [JsonPropertyName("resultSet")]
        public ResultSet ResultSet { get; set; }

        [JsonPropertyName("matches")]
        public List<Match> Matches { get; set; }
    }
}

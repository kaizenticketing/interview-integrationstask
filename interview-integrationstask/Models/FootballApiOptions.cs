namespace interview_integrationstask.Models 
{
    /// <summary>
    /// Configuration optiosn for the Football API integration
    /// </summary>
    public class FootballApiOptions 
    {
        /// <summary>
        /// API key for authentication with football-data.org 
        /// </summary>
        public string ApiKey { get; init; } = string.Empty;

        /// <summary>
        /// Base URL for the football-data.org API 
        /// </summary>
        public string BaseUrl { get; init; } = string.Empty; 

        public const string ConfigSection = "FootballApi";
    }
}
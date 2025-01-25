using System.Threading.Tasks; 

namespace interview_integrationstask.Services 
{
    /// <summary>
    /// Interface for football data API operations 
    /// </summary>
    public interface IFootballApiService 
    {
        /// <summary>
        /// Retrieves a list of all available teams 
        /// </summary> 
        /// <returns> Collection of football teams</returns>
        Task<IEnumerable<dynamic>> GetTeamsAsync();

        /// <summmary>
        /// Retrieves detailed information about a specific team 
        /// </summary>
        /// <param name="name">Name of the team to retrieve</param>
        /// /// <returns>Detailed team information</returns>
        Task<dynamic> GetTeamsByNameAsync(string name);

        /// <summary>
        /// Retrieves information about a specific leage 
        /// </summary>
        /// <param name="name">Name of the league to retrieve</param>
        /// <returns>League information</returns>
        Task<dynamic> GetLeageByNameAsync(string name);

        /// <summary>
        /// Retrieves upcoming fixtures 
        /// </summary>
        /// Mreturns>Collection of upcoming fixtures</returns> 
        Task<IEnumerable<dynamic>> GetFixturesAsync();

        /// <summary>
        /// Retrieves latest scores 
        /// </summary>
        /// <returns>Collection of latest match scores</returns>
        Task<IEnumerable<dynamic>> GetScoresAsync();
    }
}
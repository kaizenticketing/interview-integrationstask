using System.Threading.Tasks; 
using interview_integrationstask.Models;

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
        /// <returns> Paginated collection of football teams</returns>
        Task<ApiPaginatedResponse<IEnumerable<Team>>> GetTeamsAsync();

        /// <summmary>
        /// Retrieves detailed information about a specific team 
        /// </summary>
        /// <param name="name">Name of the team to retrieve</param>
        /// /// <returns>Detailed team information</returns>
        Task<Team> GetTeamsByIdAsync(int Id);

        /// <summary>
        /// Retrieves information about a specific league 
        /// </summary>
        /// <param name="name">Name of the league to retrieve</param>
        /// <returns>League information</returns>
        Task<Competition> GetLeagueByNameAsync(string name);

        /// <summary>
        /// Retrieves upcoming fixtures 
        /// </summary>
        /// Mreturns>Collection of upcoming fixtures</returns> 
        Task<ApiPaginatedResponse<IEnumerable<Match>>> GetFixturesAsync();

        /// <summary>
        /// Retrieves latest scores 
        /// </summary>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="dateFrom">Start date in yyyy-MM-dd format.</param>
        /// <param name="dateTo">End date in yyyy-MM-dd format.</param>
        /// <returns>Collection of latest match scores</returns>
        Task<ApiPaginatedResponse<IEnumerable<Match>>> GetScoresAsync(int teamId, string dateFrom, string dateTo);
    }
}
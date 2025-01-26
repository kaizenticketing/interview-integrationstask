using System.Threading.Tasks; 
using interview_integrationstask.Models;

namespace interview_integrationstask.Services 
{
    /// <summary>
    /// Interface for football data API operations 
    /// </summary>
    public interface IFootballApiService 
    {

        /// <summmary>
        /// Retrieves detailed information about a specific team 
        /// </summary>
        /// <param name="name">Name of the team to retrieve</param>
        /// /// <returns>Detailed team information</returns>
        Task<Team> GetTeamsByIdAsync(int Id);

        /// <summary>
        /// Retrieves latest scores 
        /// </summary>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="dateFrom">Start date in yyyy-MM-dd format.</param>
        /// <param name="dateTo">End date in yyyy-MM-dd format.</param>
        /// <returns>Collection of latest match scores</returns>
        Task<MatchesApiResponse> GetScoresAsync(int teamId, string dateFrom, string dateTo);
    }
}
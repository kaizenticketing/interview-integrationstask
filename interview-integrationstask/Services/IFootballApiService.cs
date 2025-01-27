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
        /// <param name="Id">ID of the team to retrieve. Includes information on their league.</param>
        /// /// <returns>Detailed team information including their league</returns>
        Task<Team> GetTeamsByIdAsync(int Id);

        /// <summary>
        /// Retrieves latest scores 
        /// </summary>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="dateFrom">Start date in yyyy-MM-dd format.</param>
        /// <param name="dateTo">End date in yyyy-MM-dd format.</param>
        /// <returns>Collection of latest match scores</returns>
        Task<MatchesApiResponse> GetScoresAsync(int teamId, string dateFrom, string dateTo);

        /// <summary>
        /// Retrieves detailed information about a specific competition (league).
        /// </summary>
        /// <param name="competitionCode">The competition code (e.g., "PL" for Premier League).</param>
        /// <returns>Detailed competition information, including seasons and teams.</returns>
        Task<Competition?> GetCompetitionAsync(string competitionCode);
    }
}
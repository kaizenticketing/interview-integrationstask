using interview_integrationstask.Models;

namespace interview_integrationstask.Interfaces.Services
{
    public interface IFootballService
    {
        Task<TeamInfo> GetTeamById(int teamId, int? season = null);
        Task<MatchInfo> GetRecentMatchesByTeam(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null);
        Task<MatchInfo> GetUpcomingFixturesByTeam(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null);
    }
}

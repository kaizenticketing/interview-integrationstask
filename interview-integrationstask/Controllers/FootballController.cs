using interview_integrationstask.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace interview_integrationstask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballController : ControllerBase
    {
        private readonly IFootballService _footballService;

        public FootballController(IFootballService footballService)
        {
            _footballService = footballService;
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId, int? season = null)
        {
            try
            {
                var teamInfo = await _footballService.GetTeamById(teamId, season);

                return Ok(teamInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("matches/recent/{teamId}")]
        public async Task<IActionResult> GetRecentMatches(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null)
        {
            try
            {
                var matches = await _footballService.GetRecentMatchesByTeam(teamId, dateFrom, dateTo, season, venue, limit);
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("matches/upcoming/{teamId}")]
        public async Task<IActionResult> GetUpcomingFixturesByTeam(int teamId, DateTime? dateFrom = null, DateTime? dateTo = null, int? season = null, string? venue = null, int? limit = null)
        {
            try
            {
                var fixtures = await _footballService.GetUpcomingFixturesByTeam(teamId, dateFrom, dateTo, season, venue, limit);
                return Ok(fixtures);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using interview_integrationstask.Services;

namespace interview_integrationstask.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FootballController: ControllerBase
    {
        private readonly IFootballApiService _footballApiService; 
        private readonly ILogger<FootballController> _logger;

        public FootballController(IFootballApiService footballApiService, ILogger<FootballController> logger)
        {
            _footballApiService = footballApiService; 
            _logger = logger; 
        } 

        /// <summary>
        /// Retrieves all available football teams
        /// </summary>
        /// <response code="200">Returns the list of teams</response>
        /// <response code="429">Rate limit exceeded</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            try 
            {
                var teams = await _footballApiService.GetTeamsAsync();
                return Ok(teams);
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teams");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving teams");
            }
        }

        /// <summary>
        ///  Retrieves information about a specific team by name
        /// </summary>
        /// <param name="name">The name of the team to retrieve</param>
        [HttpGet("teams/{name}")]
        public async Task<IActionResult> GetTeamByName([FromRoute] string name)
        {
            try 
            {
                var team = await _footballApiService.GetTeamsByNameAsync(name);
                return Ok(team);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Team '{name}' not found");
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
        }

        /// <summary>
        ///  Retrieves information about a specific league by name
        /// </summary>
        /// <param name="name">The name of the league to retrieve</param>
        [HttpGet("league/{name}")]
        public async Task<IActionResult> GetLeagueByName([FromRoute] string name)
        {
            try 
            {
                var league = await _footballApiService.GetLeageByNameAsync(name);
                return Ok(league);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"League '{name}' not found");
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
        }

        /// <summary>
        ///  Retries upcoming fixtures
        /// </summary>

        [HttpGet("fixtures")]
        public async Task<IActionResult> GetFixtures()
        {
            var fixtures = await _footballApiService.GetFixturesAsync();
            return Ok(fixtures);
        }

        [HttpGet("scores")]
        public async Task<IActionResult> GetScores()
        {
            var scores = await _footballApiService.GetScoresAsync();
            return Ok(scores);
        }

    }

}
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
        ///  Retrieves information about a specific team by Id
        /// </summary>
        /// <param name="id">The id of the team to retrieve</param>
        [HttpGet("teams/{id}")]
        public async Task<IActionResult> GetTeamById([FromRoute] int id)
        {
            try 
            {
                var team = await _footballApiService.GetTeamsByIdAsync(id);
                return Ok(team);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Team '{id}' not found");
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving team {TeamName}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving team information");
            }
        }

        /// <summary>
        ///  Retrieves information about the scores of a specific team by Id
        /// </summary>
        /// <param name="id">The id of the team to retrieve</param>
        /// <param name="dateFrom">Start date (yyyy-MM-dd). Defaults to 30 days ago if not provided.</param>
        /// <param name="dateTo"> End date (yyyy-MM-dd). Defaults to today if not provided.</param>
        [HttpGet("scores")]
        public async Task<IActionResult> GetScores([FromQuery] int? teamId = null, [FromQuery] string? dateFrom = null, [FromQuery] string? dateTo = null)
        {
            try
            {
                // Default dateFrom to 30 days ago if not provided
                dateFrom ??= DateTime.UtcNow.AddDays(-30).ToString("yyyy-MM-dd");

                // Default dateTo to today if not provided
                dateTo ??= DateTime.UtcNow.ToString("yyyy-MM-dd");

                
                _logger.LogInformation($"Retrieving scores for teamId: {teamId ?? 0}, dateFrom: {dateFrom}, dateTo: {dateTo}");

                // Validate that teamId is provided if required
                if (!teamId.HasValue)
                {
                    return BadRequest("Team ID must be provided.");
                }

                // Call the service with the provided parameters
                var scores = await _footballApiService.GetScoresAsync(teamId.Value, dateFrom, dateTo);

                return Ok(scores);
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving scores for teamId {TeamId} from {DateFrom} to {DateTo}", teamId, dateFrom, dateTo);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving scores");
            }
        }

        /// <summary>
        /// Retrieves information about a specific competition by code 
        /// </summary>
        /// <param name="code">The competition code (e.g., "PL" for Premier League)</param>
        /// <returns>Detailed competition information</returns>
        [HttpGet("competitions/{code}")]
        public async Task<IActionResult> GetCompetition([FromRoute] string code)
        {
            try 
            {
                _logger.LogInformation("Retrieving competition information for {CompetitionCode}", code);

                // Call the service to get competition details 
                var competition = await _footballApiService.GetCompetitionAsync(code);

                if (competition == null)
                {
                    return NotFound($"Competition with code '{code}' not found.");
                }

                return Ok(competition);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Competition with code '{code}' not found.");
            }
            catch (RateLimitRejectedException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving competition {CompetitionCode}", code);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving competition information.");
            }
        }

    }

}
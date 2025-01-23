using Microsoft.AspNetCore.Mvc;

namespace interview_integrationstask.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FootballController: ControllerBase
    {

        [HttpGet("teams")]
        public IActionResult GetTeams()
        {
            return Ok("Teams:");
        }

        [HttpGet("teams/{name}")]
        public IActionResult GetTeamByName()
        {
            return Ok("Team:");
        }

        [HttpGet("league/{name}")]
        public IActionResult GetLeagueByName()
        {
            return Ok("League:");
        }

        [HttpGet("fixtures")]
        public IActionResult GetFixtures()
        {
            return Ok("Fixtures:");
        }

        [HttpGet("scores")]
        public IActionResult GetScores()
        {
            return Ok("Scores:");
        }

    }

}
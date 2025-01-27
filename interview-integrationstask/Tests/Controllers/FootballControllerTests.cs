using System;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using interview_integrationstask.Controllers; 
using interview_integrationstask.Models; 
using interview_integrationstask.Services; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging; 
using Moq;
using Xunit; 

namespace interview_integrationstask.Tests.Controllers 
{
    public class FootballControllerTests 
    {
        private readonly Mock<IFootballApiService> _footballApiServiceMock; 
        private readonly Mock<ILogger<FootballController>> _loggerMock; 
        private readonly FootballController _controller; 

        public FootballControllerTests()
        {
            _footballApiServiceMock = new Mock<IFootballApiService>();
            _loggerMock = new Mock<ILogger<FootballController>>();
            _controller = new FootballController(_footballApiServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTeambyId_ReturnsOk_WhenTeamIsFound()
        {
            // Arrange 
            var team = new Team { Id = 1, Name = "Team A"};
            _footballApiServiceMock.Setup(s => s.GetTeamsByIdAsync(1))
                .ReturnsAsync(team);
            
            // Act 
            var result = await _controller.GetTeamById(1);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(team, okResult.Value);
        }

        [Fact]
        public async Task GetTeamById_ReturnsNotFound_WhenTeamIsNotFound()
        {
            // Arrange 
            _footballApiServiceMock.Setup(s => s.GetTeamsByIdAsync(1))
                .ThrowsAsync(new KeyNotFoundException());
            
            // Act 
            var result = await _controller.GetTeamById(1);

            // Assert 
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal("Team '1' not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetTeamById_ReturnsTooManyRequests_WhenRateLimitExceeded()
        {
            // Arrange 
            _footballApiServiceMock.Setup(s => s.GetTeamsByIdAsync(1))
                .ThrowsAsync(new RateLimitRejectedException("Rate limit exceeded"));
            
            // Act 
            var result = await _controller.GetTeamById(1);

            // Assert 
            var tooManyRequestsResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status429TooManyRequests, tooManyRequestsResult.StatusCode);
            Assert.Equal("Rate limit exceeded", tooManyRequestsResult.Value);
        }

        [Fact]
        public async Task GetTeamById_ReturnsInternalServerError_OnUnexpectedException()
        {
            // Arrange 
            _footballApiServiceMock.Setup(s => s.GetTeamsByIdAsync(1))
                .ThrowsAsync(new Exception("Unexpected error"));
            
            // Act 
            var result = await _controller.GetTeamById(1);

            // Assert 
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
            Assert.Equal("An error occurred while retrieving team information", internalServerErrorResult.Value);
        }

        [Fact]
        public async Task GetScores_ReturnsOk_WhenScoresAreFound()
        {
            // Arrange 
            var scores = new MatchesApiResponse 
            {
                Matches = new List<Models.Match>
                {
                    new interview_integrationstask.Models.Match { Id = 101, Status = "FINISHED"}
                }
            };
            _footballApiServiceMock.Setup(s => s.GetScoresAsync(1, "2023-01-01", "2023-12-31"))
                .ReturnsAsync(scores);
            
            // Act 
            var result = await _controller.GetScores(1, "2023-01-01", "2023-12-31");

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(scores, okResult.Value);
        }

        [Fact]
        public async Task GetScores_ReturnsBadRequest_WhenTeamIdIsMissing()
        {
            // Act 
            var result = await _controller.GetScores(null, "2023-01-01", "2023-12-31");

            // Assert 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Team ID must be provided.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetScores_ReturnsInternalServerError_OnUnexpectedException()
        {
            // Arrange 
            _footballApiServiceMock.Setup(s => s.GetScoresAsync(1, "2023-01-01", "2023-12-31"))
                .ThrowsAsync(new Exception("Unexpected error"));
            
            // Act 
            var result = await _controller.GetScores(1, "2023-01-01", "2023-12-31");

            // Assert 
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);
            Assert.Equal("An error occurred while retrieving scores", internalServerErrorResult.Value);
        }
    }
}
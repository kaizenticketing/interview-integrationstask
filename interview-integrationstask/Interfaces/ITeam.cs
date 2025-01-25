using System.Collections.Generic; 

namespace interview_integrationstask.Interfaces 
{
    /// <summary>
    /// Interface for a football team.
    /// </summary>
    public interface ITeam 
    {
        int Id { get; }
        string Name { get; }
        string? ShortName { get; }
        string? Tla { get; }
        string? CrestUrl { get; }
        int? Founded { get; }
        string? Venue { get; }
        IEnumerable<ICompetition> RunningCompetitions { get; }
    }
}
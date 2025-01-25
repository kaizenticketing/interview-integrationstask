using interview_integrationstask.Models;

namespace interview_integrationstask.Interfaces 
{
    /// <summary>
    /// Interface for score details of a football match. 
    /// </summary>
    public interface IScore 
    {
        string Winner { get; }
        string Duration { get; }
        PeriodScore FullTime { get; }
        PeriodScore HalfTime { get; }
    }
}
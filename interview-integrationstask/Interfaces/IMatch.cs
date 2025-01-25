namespace interview_integrationstask.Interfaces 
{
    /// <summary>
    /// Interface for a football match/fixture. 
    /// </summary>
    public interface IMatch 
    {
        int Id { get; }
        ICompetition Competition { get; }
        ITeam HomeTeam { get; }
        ITeam AwayTeam { get; }
        DateTime UtcDate { get; }
        string Status { get; }
        IScore Score { get; }
    }
}
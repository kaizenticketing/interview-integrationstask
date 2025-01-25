namespace interview_integrationstask.Interfaces 
{
    /// <summary>
    /// Interface for a football competition. 
    /// </summary>
    public interface ICompetition 
    {
        int Id { get; }
        string Name { get; }
        string Code { get; }
        string Type { get; }
        string? EmblemUrl { get; }
    }
}
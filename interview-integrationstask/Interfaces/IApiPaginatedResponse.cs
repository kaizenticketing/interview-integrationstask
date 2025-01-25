namespace interview_integrationstask.Interfaces 
{
    /// <summary>
    /// Interface for paginated API responses 
    /// </summary>
    public interface IApiPaginedResponse<T> 
    {
        int Count { get; }

        Dictionary<string, object>? Filters { get; }

        T ResultSet { get; }
    }
}
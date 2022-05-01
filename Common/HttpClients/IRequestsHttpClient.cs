namespace RequestService.Common.HttpClients;

/// <summary>
/// Provides methods to access RequestsController API methods.
/// </summary>
public interface IRequestsHttpClient
{
    /// <summary>
    /// Calls Api SubmitRequest method.
    /// </summary>
    /// <param name="origin">Origin of the request.</param>
    public Task SubmitRequestAsync(int origin);
}
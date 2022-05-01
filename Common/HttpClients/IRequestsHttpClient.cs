using RequestService.Common.Models;

namespace RequestService.Common.HttpClients;

/// <summary>
/// Provides methods to access RequestsController API methods.
/// </summary>
public interface IRequestsHttpClient
{
    /// <summary>
    /// Calls Api GetRequest method.
    /// </summary>
    /// <param name="origin">Origin of the request.</param>
    /// <returns><see cref="Request"/> result.</returns>
    public Task<Request> GetRequestAsync(int origin);
}
using RequestService.Common.Models;

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

    /// <summary>
    /// Calls Api GetAllRequests method.
    /// </summary>
    /// <returns><see cref="Request"/> collection.</returns>
    public Task<IEnumerable<Request>> GetAllRequestsAsync();

    /// <summary>
    /// Calls Api GetSummary method.
    /// </summary>
    /// <returns><see cref="RequestsPerHourSummary"/> collection.</returns>
    public Task<IEnumerable<RequestsPerHourSummary>> GetSummaryAsync();

    /// <summary>
    /// Calls Api GetGeneratedRequests method.
    /// </summary>
    /// <returns><see cref="Request"/> collection.</returns>
    public Task<IEnumerable<Request>> GetGeneratedRequestsAsync();

    /// <summary>
    /// Calls Api GetGeneratedSummary method.
    /// </summary>
    /// <returns><see cref="RequestsPerHourSummary"/> collection.</returns>
    public Task<IEnumerable<RequestsPerHourSummary>> GetGeneratedSummaryAsync();
}
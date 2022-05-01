using RequestService.Common.Models;
using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Services;

/// <summary>
/// Provides interface for working with <see cref="Request"/> instances.
/// </summary>
public interface IRequestService
{
    /// <summary>
    /// Generates request.
    /// </summary>
    /// <param name="route"><see cref="Route"/> of the generated request.</param>
    /// <returns><see cref="Request"/> instance.</returns>
    public Request GenerateRequest(Route route);

    /// <summary>
    /// Generates request and saves it to database.
    /// </summary>
    /// <param name="route"><see cref="Route"/> of the generated request.</param>
    public void GenerateAndSaveRequest(Route route);

    /// <summary>
    /// Retrieves all requests from database.
    /// </summary>
    /// <returns>All <see cref="Request"/> items from database.</returns>
    public IEnumerable<Request> GetAllRequestsFromDatabase();

    /// <summary>
    /// Generates daily requests.
    /// </summary>
    /// <returns>Generated <see cref="Request"/> items.</returns>
    public IEnumerable<Request> GenerateRequests(int routesCount);

    /// <summary>
    /// Reads requests from database and creates summary.
    /// </summary>
    /// <returns><see cref="RequestsPerHourSummary"/> collection.</returns>
    public IEnumerable<RequestsPerHourSummary> GetRequestsSummary();
}
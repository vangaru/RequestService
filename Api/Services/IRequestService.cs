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
}
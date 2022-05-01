using RequestService.Common;

namespace RequestService.Api.Services;

/// <summary>
/// Provides interface for working with <see cref="Request"/> instances.
/// </summary>
public interface IRequestService
{
    /// <summary>
    /// Generates request.
    /// </summary>
    /// <param name="route"><see cref="Common.Route"/> of the generated request.</param>
    /// <returns><see cref="Request"/> instance.</returns>
    public Request GenerateRequest(Common.Route route);
}
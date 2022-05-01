using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Services;

/// <summary>
/// Provides interface for working with <see cref="Route"/> routes.
/// </summary>
public interface IRouteService
{
    /// <summary>
    /// Generates random <see cref="Route"/> instance.
    /// </summary>
    /// <param name="routesCount">Count of accessible routes.</param>
    /// <returns>Generates <see cref="Route"/> instance.</returns>
    public Route GenerateRandomRoute(int routesCount);
}
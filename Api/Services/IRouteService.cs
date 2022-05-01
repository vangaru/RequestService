using Route = RequestService.Common.Route;

namespace RequestService.Api.Services;

/// <summary>
/// Provides interface for working with <see cref="Route"/> routes.
/// </summary>
public interface IRouteService
{
    /// <summary>
    /// Generates random <see cref="Route"/> instance.
    /// </summary>
    /// <param name="origin">Origin of the route.</param>
    /// <param name="routesCount">Count of accessible routes.</param>
    /// <returns>Generates <see cref="Route"/> instance.</returns>
    public Route GenerateRandomRoute(int origin, int routesCount);
}
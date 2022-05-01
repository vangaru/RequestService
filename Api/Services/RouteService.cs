using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Services;

/// <summary>
/// Implementation of <see cref="RouteService"/>.
/// </summary>
public class RouteService : IRouteService
{
    private readonly Random _random = new();

    /// <inheritdoc cref="IRouteService.GenerateRandomRoute"/>
    public Route GenerateRandomRoute(int origin, int routesCount)
    {
        if (routesCount <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(routesCount), "Cannot be less than 2");
        }

        if (origin < 1 || origin > routesCount)
        {
            throw new ArgumentOutOfRangeException(nameof(origin), "Must be inside 1 and routesCount bounds");
        }

        int destination = GetDestination(origin, routesCount);

        string routeId = Guid.NewGuid().ToString();
        var route = new Route
        {
            Id = routeId,
            Origin = origin,
            Destination = destination
        };

        return route;
    }

    private int GetDestination(int origin, int routesCount)
    {
        int destination = _random.Next(1, routesCount);

        if (origin == destination)
        {
            if (origin == routesCount)
            {
                destination = origin - 1;
            }
            else
            {
                destination = origin + 1;
            }
        }

        return destination;
    }
}
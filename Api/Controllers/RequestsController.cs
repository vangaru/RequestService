using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using RequestService.Api.Services;
using RequestService.Common.Models;
using Route = RequestService.Common.Models.Route;

namespace RequestService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestsController : ControllerBase
{
    private const string OriginParameter = "origin";
    private const string RoutesCountParameter = "routesCount";

    private readonly IRouteService _routeService;
    private readonly IRequestService _requestService;

    public RequestsController(IRouteService routeService, IRequestService requestService)
    {
        _routeService = routeService;
        _requestService = requestService;
    }

    [HttpPost]
    public IActionResult SubmitRequest([FromBody] JsonObject routeData)
    {
        int origin = routeData[OriginParameter]!.GetValue<int>();
        int routesCount = routeData[RoutesCountParameter]!.GetValue<int>();
        Route route = _routeService.GenerateRandomRoute(routesCount, origin);
        _requestService.GenerateAndSaveRequest(route);
        return NoContent();
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAllRequest()
    {
        IEnumerable<Request> requests = _requestService.GetAllRequestsFromDatabase();
        return Ok(requests);
    }
}
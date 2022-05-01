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

    [HttpGet]
    [Route("all")]
    public IActionResult GetAllRequest()
    {
        IEnumerable<Request> requests = _requestService.GetAllRequestsFromDatabase();
        return Ok(requests);
    }

    [HttpGet]
    [Route("summary")]
    public IActionResult GetSummary()
    {
        IEnumerable<RequestsPerHourSummary> summary = _requestService.GetRequestsSummary();
        return Ok(summary);
    }

    [HttpPost]
    [Route("generated/requests")]
    public IActionResult GetGeneratedRequests([FromBody] int routesCount)
    {
        IEnumerable<Request> requests = _requestService.GenerateRequests(routesCount);
        return Ok(requests);
    }

    [HttpPost]
    [Route("generated/summary")]
    public IActionResult GetGeneratedSummary([FromBody] int routesCount)
    {
        IEnumerable<RequestsPerHourSummary> summary = _requestService.GenerateSummary(routesCount);
        return Ok(summary);
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
}
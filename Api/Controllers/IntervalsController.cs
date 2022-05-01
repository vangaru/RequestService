using Microsoft.AspNetCore.Mvc;
using RequestService.Api.Services;

namespace RequestService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IntervalsController : ControllerBase
{
    private readonly IIntervalService _intervalService;

    public IntervalsController(IIntervalService intervalService)
    {
        _intervalService = intervalService;
    }

    [HttpGet]
    public IActionResult GetCurrentInterval()
    {
        return Ok(_intervalService.CurrentInterval);
    }
}
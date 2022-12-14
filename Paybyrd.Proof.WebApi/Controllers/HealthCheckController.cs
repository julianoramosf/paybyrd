using Microsoft.AspNetCore.Mvc;
namespace Paybyrd.Proof.WebApi.Controllers;
[ApiController]
[Route("/v1/hc")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<DiffController> _logger;

    public HealthCheckController(ILogger<DiffController> logger)
    {
        _logger = logger;
    }

 
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok("ApiOnline");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

}
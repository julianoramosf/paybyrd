using Microsoft.AspNetCore.Mvc;
using PayByrd.Proof.Bussiness.Interfaces;
using PayByrd.Proof.Bussiness.Constants;
namespace PayByrd.Proof.WebApi.Controllers;
[ApiController]
[Route("/v1/diff")]
public class DiffController : ControllerBase
{
    private readonly ILogger<DiffController> _logger;
    private readonly IDiffService _service;

    public DiffController(ILogger<DiffController> logger, IDiffService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("{id}/left")]
    public IActionResult Left(Guid id, [FromBody]BaseModel model)
    {
        try
        {
            _service.SaveDataObject(id, Global.LEFTDIFF, model.Data);
            return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("{id}/right")]
    public IActionResult Right(Guid id, [FromBody]BaseModel model)
    {
        try
        {
            _service.SaveDataObject(id, Global.RIGHTDIFF, model.Data);
           return Ok();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var result = _service.ProcessResult(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex);
        }
    }

}
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PayByrd.Proof.WebApi;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PayByrd.Proof.WebApi.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class DiffController : ControllerBase
{
    private readonly ILogger<DiffController> _logger;

    public DiffController(ILogger<DiffController> logger)
    {
        _logger = logger;
    }

    [HttpPost("{id}/left")]
    public async Task<string> Left(string id)
    {
        var json = String.Empty;
        JObject leftObject;

        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            var jsonBase64 = await reader.ReadToEndAsync();
            var blob = Convert.FromBase64String(jsonBase64);
            json = Encoding.UTF8.GetString(blob);
            leftObject = JsonConvert.DeserializeObject<JObject>(json) ?? new JObject();
        }


        // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"

        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"
        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        return json;

    }

    [HttpPost("{id}/right")]
    public async Task<string> Right(string id)
    {
        var json = String.Empty;

        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            var jsonBase64 = await reader.ReadToEndAsync();
            var blob = Convert.FromBase64String(jsonBase64);
            json = Encoding.UTF8.GetString(blob);
        }

        // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"

        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"
        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        return json;

    }

    [HttpPost("{id}")]
    public async Task<string> Post(string id)
    {
        var json = String.Empty;

        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            var jsonBase64 = await reader.ReadToEndAsync();
            var blob = Convert.FromBase64String(jsonBase64);
            json = Encoding.UTF8.GetString(blob);
        }
        
        // JObject
        
                // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"

        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        // var base64 = @"e0tleSA6ICdhYmMnLCBpc0V4aXN0czogJ3RydWUnfQ==";
        // var blob = Convert.FromBase64String(base64);
        // var json = Encoding.UTF8.GetString(blob);
        // // string: "{Key : 'abc', isExists: 'true'}"
        // var obj = JsonConvert.DeserializeObject<MyPayload>(json);

        return json;

    }

}
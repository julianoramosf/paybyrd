using System.Net;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Paybyrd.Proof.Bussiness.Constants;
using Paybyrd.Proof.Bussiness.Models;

namespace Paybyrd.IntegrationTest;

public class WebApiTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WebApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/v1/hc")]
    public async Task Get_Endpoint_Return_Success_HealthCheck(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        Assert.Equal("ApiOnline", content);
    }


    [Theory]
    [InlineData("/v1/diff/eb6c8908-ba6c-4d65-a53d-53b7a2bf139b/left")]
    [InlineData("/v1/diff/eb6c8908-ba6c-4d65-a53d-53b7a2bf139b/right")]
    public async Task Post_Endpoint_ReturnSuccess_For_Correct_SaveDataObject(string url)
    {
        var client = _factory.CreateClient();
        //Example: {"Name": "João","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64Data = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIlN0YXRlIjogIlJpbyBkZSBKYW5laXJvIn0=";
        var data = JsonConvert.SerializeObject(new { data = jsonBase64Data });
        using var content = new StringContent(data, Encoding.UTF8, "application/json");
        
        var id = Guid.NewGuid().ToString();

        using var response = await client.PostAsync(url, content);
            
        Assert.True(response.IsSuccessStatusCode);
    }


    [Fact]
    public async Task Get_Endpoint_ReturnSuccess_For_JsonEquals()
    {
        var client = _factory.CreateClient();
        var id = Guid.NewGuid().ToString();

        //Example: {"Name": "João","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64Data = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIlN0YXRlIjogIlJpbyBkZSBKYW5laXJvIn0=";
        var data = JsonConvert.SerializeObject(new { data = jsonBase64Data });
        using var content = new StringContent(data, Encoding.UTF8, "application/json");

        var saveLeft = await client.PostAsync($"/v1/diff/{id}/left", content);
        var saveRight = await client.PostAsync($"/v1/diff/{id}/right", content);

        var response = await client.GetAsync($"/v1/diff/{id}");
        var jsonReturn = await response.Content.ReadAsStringAsync();

        var resultModel = JsonConvert.DeserializeObject<ResponseDiffDataModel>(jsonReturn);
                
        Assert.Equal(Messages.JSON_EQUALS, resultModel.Message);
    }


    [Fact]
    public async Task Get_Endpoint_ReturnSuccess_For_JsonSizeNotEquals()
    {
        var client = _factory.CreateClient();
        var id = Guid.NewGuid().ToString();

        //Example: {"Name": "João","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64DataLeft = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIlN0YXRlIjogIlJpbyBkZSBKYW5laXJvIn0=";
        var dataLeft = JsonConvert.SerializeObject(new { data = jsonBase64DataLeft });
        using var contentLeft = new StringContent(dataLeft, Encoding.UTF8, "application/json");

        //Example: {"Name": "João","Age": 41,"City": "Rio de Janeiro","State": "RJ"}
        var jsonBase64DataRight = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIkNpdHkiOiAiUmlvIGRlIEphbmVpcm8iLCJTdGF0ZSI6ICJSSiJ9";
        var dataRight = JsonConvert.SerializeObject(new { data = jsonBase64DataRight });
        using var contentRight = new StringContent(dataRight, Encoding.UTF8, "application/json");

        var saveLeft = await client.PostAsync($"/v1/diff/{id}/left", contentLeft);
        var saveRight = await client.PostAsync($"/v1/diff/{id}/right", contentRight);

        var response = await client.GetAsync($"/v1/diff/{id}");
        var jsonReturn = await response.Content.ReadAsStringAsync();

        var resultModel = JsonConvert.DeserializeObject<ResponseDiffDataModel>(jsonReturn);

        Assert.Equal(Messages.JSON_SIZE_NOT_EQUALS, resultModel.Message);
    }


    [Fact]
    public async Task Get_Endpoint_ReturnSuccess_For_HasListValuesDiff()
    {
        var client = _factory.CreateClient();
        var id = Guid.NewGuid().ToString();

        //Example: {"Name": "João","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64DataLeft = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIlN0YXRlIjogIlJpbyBkZSBKYW5laXJvIn0=";
        var dataLeft = JsonConvert.SerializeObject(new { data = jsonBase64DataLeft });
        using var contentLeft = new StringContent(dataLeft, Encoding.UTF8, "application/json");

        //Example: {"Name": "Amanda","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64DataRight = "IHsiTmFtZSI6ICJBbWFuZGEiLCJBZ2UiOiA0MSwiU3RhdGUiOiAiUmlvIGRlIEphbmVpcm8ifQ==";
        var dataRight = JsonConvert.SerializeObject(new { data = jsonBase64DataRight });
        using var contentRight = new StringContent(dataRight, Encoding.UTF8, "application/json");

        var saveLeft = await client.PostAsync($"/v1/diff/{id}/left", contentLeft);
        var saveRight = await client.PostAsync($"/v1/diff/{id}/right", contentRight);

        var response = await client.GetAsync($"/v1/diff/{id}");
        var jsonReturn = await response.Content.ReadAsStringAsync();

        var resultModel = JsonConvert.DeserializeObject<ResponseDiffDataModel>(jsonReturn);

        Assert.True(resultModel.ListDiffs.Values.Count > 0);
    }


    [Fact]
    public async Task Get_Endpoint_ReturnSuccess_For_HasListPropertiesDiff()
    {
        var client = _factory.CreateClient();
        var id = Guid.NewGuid().ToString();

        //Example: {"Name": "João","Age": 41,"State": "Rio de Janeiro"}
        var jsonBase64DataLeft = "eyJOYW1lIjogIkpvw6NvIiwiQWdlIjogNDEsIlN0YXRlIjogIlJpbyBkZSBKYW5laXJvIn0=";
        var dataLeft = JsonConvert.SerializeObject(new { data = jsonBase64DataLeft });
        using var contentLeft = new StringContent(dataLeft, Encoding.UTF8, "application/json");

        //Example: {"Name": "Amanda","Age": 41,"City": "Rio de Janeiro"}
        var jsonBase64DataRight = "ICAgICB7Ik5hbWUiOiAiQW1hbmRhIiwiQWdlIjogNDEsIkNpdHkiOiAiUmlvIGRlIEphbmVpcm8ifQ==";
        var dataRight = JsonConvert.SerializeObject(new { data = jsonBase64DataRight });
        using var contentRight = new StringContent(dataRight, Encoding.UTF8, "application/json");

        var saveLeft = await client.PostAsync($"/v1/diff/{id}/left", contentLeft);
        var saveRight = await client.PostAsync($"/v1/diff/{id}/right", contentRight);

        var response = await client.GetAsync($"/v1/diff/{id}");
        var jsonReturn = await response.Content.ReadAsStringAsync();

        var resultModel = JsonConvert.DeserializeObject<ResponseDiffDataModel>(jsonReturn);

        Assert.True(resultModel.ListDiffs.Properties.Count > 0);
    }


}

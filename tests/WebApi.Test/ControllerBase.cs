using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Tasks.Communication.Request;
using Tasks.Execptions;

namespace WebApi.Test;
public class ControllerBase : IClassFixture<TasksWebApplicationFactory<Program>>
{

    private readonly HttpClient _httpClient;
    public ControllerBase(TasksWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        ResourceErrorsMessage.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PostAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string method, object body, string token = "")
    {
        AuthorizeRequest(token);
        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PutAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<string> Login(string email, string password)
    {
        var request = new RequestLogin
        {
            Email = email,
            Password = password
        };

        var response = await PostRequest("usuario/login", request);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        return responseData.RootElement.GetProperty("token").GetString();
    }

    private void AuthorizeRequest(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }
}
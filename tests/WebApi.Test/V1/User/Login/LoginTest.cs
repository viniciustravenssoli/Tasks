using FluentAssertions;
using System.Net;
using System.Text.Json;
using Tasks.Communication.Request;
using Tasks.Execptions;

namespace WebApi.Test.V1.User.Login;
public class LoginTest : ControllerBase
{
    private const string METHOD = "usuario/login";

    private Tasks.Domain.Entities.User _user;
    private string _password;
    public LoginTest(TasksWebApplicationFactory<Program> factory) : base(factory)
    {
        _user = factory.GetUsuario();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task ValidateSuccess()
    {
        var request = new RequestLogin
        {
            Email = _user.Email,
            Password = _password
        };

        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_user.Name);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ValidateErrorInvalidEmail()
    {
        var request = new RequestLogin
        {
            Email = "email@invalido.com",
            Password = _password
        };

        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("messages").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceErrorsMessage.InvalidLogin);
    }
}

using FluentAssertions;
using System.Net;
using System.Text.Json;
using Tasks.Execptions;
using Util.Test.Request;

namespace WebApi.Test.V1.User.ChangePassword;
public class ChangePasswordTest : ControllerBase
{
    private const string METHOD = "usuario/alterar-senha";

    private Tasks.Domain.Entities.User _user;
    private string _password;
    public ChangePasswordTest(TasksWebApplicationFactory<Program> factory) : base(factory)
    {
        _user = factory.GetUsuario();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task ValidateSuccess()
    {
        var token = await Login(_user.Email, _password);

        var request = RequestChangePasswordBuilder.Builder();
        request.CurrentPassword = _password;

        var response = await PutRequest(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task ValidateErrorEmptyPassword()
    {
        var token = await Login(_user.Email, _password);

        var request = RequestChangePasswordBuilder.Builder();
        request.CurrentPassword = _password;
        request.NewPassword = string.Empty;

        var response = await PutRequest(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceErrorsMessage.EmptyPassword));


    }
}

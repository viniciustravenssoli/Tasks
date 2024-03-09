using FluentAssertions;
using System.Net;
using System.Text.Json;
using Tasks.Execptions;
using Util.Test.Request;

namespace WebApi.Test.V1.User.Register;
public class RegisterUserTest : ControllerBase
{
    private const string METHOD = "usuario";

    public RegisterUserTest(TasksWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidateSuccess()
    {
        var request = RequestRegisterUserBuilder.Builder();

        var reponse = await PostRequest(METHOD, request);

        reponse.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await reponse.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ValidateErrorEmptyName()
    {
        var request = RequestRegisterUserBuilder.Builder();
        request.UserName = string.Empty;

        var reponse = await PostRequest(METHOD, request);

        reponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await reponse.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceErrorsMessage.EmptyUserName));
    }
}

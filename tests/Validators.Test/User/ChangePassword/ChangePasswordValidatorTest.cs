using FluentAssertions;
using Tasks.Application.UseCases.User.ChangePassword;
using Tasks.Execptions;
using Util.Test.Request;

namespace Validators.Test.User.ChangePassword;
public class ChangePasswordValidatorTest
{
    [Fact]
    public void ValidateSuccess()
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordBuilder.Builder();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();

    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ValidateErrorInvalidPassword(int passwordLength)
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordBuilder.Builder(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.PasswordMust6));

    }

    [Fact]
    public void ValidateErrorEmptyPassword()
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordBuilder.Builder();
        request.NewPassword = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyPassword));

    }
}

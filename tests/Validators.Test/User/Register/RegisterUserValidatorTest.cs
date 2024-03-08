using FluentAssertions;
using Tasks.Application.UseCases.User.Register;
using Tasks.Execptions;
using Util.Test.Request;

namespace Validators.Test.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void ValidaSuccess()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void ValidateErrorEmptyName()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.UserName = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyUserName));
    }

    [Fact]
    public void ValidateErrorEmptyEmail()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyUserEmail));
    }

    [Fact]
    public void ValidateErrorEmptyPassword()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyPassword));
    }

    [Fact]
    public void ValidateErrorEmptyPhone()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.Telefone = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyPhone));
    }

    [Fact]
    public void ValidateErrorInvalidEmail()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.Email = "vt";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.InvalidUserEmail));
    }

    [Fact]
    public void ValidateErrorInvalidPhone()
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder();
        request.Telefone = "23 2";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.InvalidPhone));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void ValidateErrorInvalidPassword(int passwordLength)
    {
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Builder(passwordLength);
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.PasswordMust6));
    }
}

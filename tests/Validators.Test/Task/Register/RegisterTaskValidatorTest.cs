using FluentAssertions;
using Tasks.Application.UseCases.Task.Register;
using Tasks.Communication.Enums;
using Tasks.Execptions;
using Util.Test.Request;

namespace Validators.Test.Task.Register;
public class RegisterTaskValidatorTest
{
    [Fact]
    public void ValidaSuccess()
    {
        var validator = new RegisterTaskValidator();

        var request = RequestRegisterTaskBuilder.Builder();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidateErrorEmptyName()
    {
        var validator = new RegisterTaskValidator();

        var request = RequestRegisterTaskBuilder.Builder();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.EmptyTaskName));
    }
    [Fact]
    public void ValidateErrorInvalidStatus()
    {
        var validator = new RegisterTaskValidator();

        var request = RequestRegisterTaskBuilder.Builder();
        request.Status = (Tasks.Communication.Enums.TaskStatus)1000;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.InvalidStatus));
    }

    [Fact]
    public void ValidateErrorInvalidPriority()
    {
        var validator = new RegisterTaskValidator();

        var request = RequestRegisterTaskBuilder.Builder();
        request.Priority = (TaskPriority)1000;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceErrorsMessage.InvalidPriority));
    }

}

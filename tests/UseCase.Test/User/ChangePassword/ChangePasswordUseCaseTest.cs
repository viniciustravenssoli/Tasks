using FluentAssertions;
using Tasks.Communication.Request;
using Tasks.Execptions.BaseExecptions;
using Tasks.Execptions;
using Util.Test.Encrypt;
using Util.Test.LoggedUser;
using Util.Test.Repositories;
using Util.Test.Entities;
using Tasks.Application.UseCases.User.ChangePassword;
using Util.Test.Request;

namespace UseCase.Test.User.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async System.Threading.Tasks.Task ValidateSuccess()
    {
        (var user, var password) = UserBuilder.Builder();
        var useCase = CreateUseCase(user);

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Executar(new RequestChangePassword
            {
                CurrentPassword = password,
                NewPassword = "123654",
            });
        };

        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErroEmptyNewPassord()
    {
        (var user, var password) = UserBuilder.Builder();
        var useCase = CreateUseCase(user);

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Executar(new RequestChangePassword
            {
                CurrentPassword = password,
                NewPassword = "",
            });
        };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorsMessage.EmptyPassword));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async System.Threading.Tasks.Task ValidateErroMinimunPasswordLength(int passwordLength)
    {
        (var user, var password) = UserBuilder.Builder();
        var useCase = CreateUseCase(user);

        var request = RequestChangePasswordBuilder.Builder(passwordLength);
        request.CurrentPassword = password;

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Executar(request);
        };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorsMessage.PasswordMust6));
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErroInvalidCurrentPassord()
    {
        (var user, var password) = UserBuilder.Builder();
        var useCase = CreateUseCase(user);

        var request = RequestChangePasswordBuilder.Builder();
        request.CurrentPassword = "senhaInvalida";

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Executar(request);
        };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorsMessage.CurrentPasswordInvalid));
    }

    private static ChangePasswordUseCase CreateUseCase(Tasks.Domain.Entities.User user)
    {
        var encrypt = PasswordEncryptBuilder.Instance();
        var unitOfWork = UnitOfWorkBuilder.Instance().Builder();
        var repository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(user).Builder();
        var loggedUser = LoggedUsuerBuilder.Instance().GetUser(user).Builder();

        return new ChangePasswordUseCase(loggedUser, repository,encrypt,unitOfWork);
    }
}

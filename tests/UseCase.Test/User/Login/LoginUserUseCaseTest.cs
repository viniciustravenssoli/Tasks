using FluentAssertions;
using Tasks.Application.UseCases.User.Login;
using Tasks.Communication.Request;
using Tasks.Execptions.BaseExecptions;
using Tasks.Execptions;
using Util.Test.TokenController;
using Util.Test.Entities;
using Util.Test.Encrypt;
using Util.Test.Repositories;

namespace UseCase.Test.User.Login;
public class LoginUserUseCaseTest
{
    [Fact]
    public async System.Threading.Tasks.Task ValidateSuccess()
    {
        (var user, var password) = UserBuilder.Builder();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute(new RequestLogin
        {
            Email = user.Email,
            Password = password
        });

        response.Should().NotBeNull();
        response.Name.Should().Be(user.Name);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorInvalidPassword()
    {
        (var user, var password) = UserBuilder.Builder();

        var useCase = CreateUseCase(user);

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Execute(new RequestLogin
            {
                Email = user.Email,
                Password = "senhaInvalida"
            });
        };

        await action.Should().ThrowAsync<LoginInvalidException>()
            .Where(exception => exception.Message.Equals(ResourceErrorsMessage.InvalidLogin));
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorInvalidEmail()
    {
        (var user, var password) = UserBuilder.Builder();

        var useCase = CreateUseCase(user);

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Execute(new RequestLogin
            {
                Email = "email@invalido.com",
                Password = password
            });
        };

        await action.Should().ThrowAsync<LoginInvalidException>()
            .Where(exception => exception.Message.Equals(ResourceErrorsMessage.InvalidLogin));
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorInvalidEmailAndPassword()
    {
        (var user, var password) = UserBuilder.Builder();

        var useCase = CreateUseCase(user);

        Func<System.Threading.Tasks.Task> action = async () =>
        {
            await useCase.Execute(new RequestLogin
            {
                Email = "email@invalido.com",
                Password = "senhaInvalida"
            });
        };

        await action.Should().ThrowAsync<LoginInvalidException>()
            .Where(exception => exception.Message.Equals(ResourceErrorsMessage.InvalidLogin));
    }

    private LoginUseCase CreateUseCase(Tasks.Domain.Entities.User user)
    {
        var encrypt = PasswordEncryptBuilder.Instance();
        var token = TokenControllerBuilder.Instance();
        var repositoryReadOnly = UserReadOnlyRepositoryBuilder.Instance().GetByEmailAndPassword(user).Builder();

        return new LoginUseCase(repositoryReadOnly, encrypt, token);
    }
}

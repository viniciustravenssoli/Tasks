using FluentAssertions;
using Tasks.Application.UseCases.User.Register;
using Tasks.Execptions;
using Tasks.Execptions.BaseExecptions;
using Util.Test.Encrypt;
using Util.Test.Repositories;
using Util.Test.Request;
using Util.Test.TokenController;

namespace UseCase.Test.User.Register;
public class RegisterUserUseCaseTest
{

    [Fact]
    public async System.Threading.Tasks.Task ValidateSuccess()
    {
        var request = RequestRegisterUserBuilder.Builder();

        var useCase = CreateUseCase();

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorEmailInUse()
    {
        var request = RequestRegisterUserBuilder.Builder();

        var useCase = CreateUseCase(request.Email);

        Func<System.Threading.Tasks.Task> action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(exception => exception.ErrorMessages.Count == 1
            && exception.ErrorMessages.Contains(ResourceErrorsMessage.EmailAlreadyInUse));
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorEmptyEmail()
    {
        var request = RequestRegisterUserBuilder.Builder();
        request.Email = string.Empty;

        var useCase = CreateUseCase();

        Func<System.Threading.Tasks.Task> action = async () => { await useCase.Execute(request); };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(exception => exception.ErrorMessages.Count == 1
            && exception.ErrorMessages.Contains(ResourceErrorsMessage.EmptyUserEmail));
    }

    private static RegisterUserUseCase CreateUseCase(string email = "")
    {
        var repository = UserWriteOnlyRepositoryBuilder.Instance().Builder();
        var unitOfWork = UnitOfWorkBuilder.Instance().Builder();
        var encrypt = PasswordEncryptBuilder.Instance();
        var token = TokenControllerBuilder.Instance();
        var repositoryReadOnly = UserReadOnlyRepositoryBuilder.Instance().ExistUsuarioWithEmail(email).Builder();

        return new RegisterUserUseCase(repositoryReadOnly, repository,unitOfWork,encrypt,token);
    }
}

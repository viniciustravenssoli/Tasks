using Bogus;
using FluentAssertions;
using Tasks.Application.UseCases.Task.Register;
using Tasks.Execptions;
using Tasks.Execptions.BaseExecptions;
using Util.Test.Entities;
using Util.Test.LoggedUser;
using Util.Test.Repositories;
using Util.Test.Request;

namespace UseCase.Test.Task.Register;
public class RegisterTaskUseCaseTest
{
    [Fact]
    public async System.Threading.Tasks.Task ValidateSuccess()
    {
        (var user, var _) = UserBuilder.Builder();

        var useCase = CreateUseCase(user);

        var request = RequestRegisterTaskBuilder.Builder();

        var response = await useCase.Execute(request);

        response.Should().NotBeNull();

        response.Id.Should().NotBeNullOrWhiteSpace();
        response.Name.Should().Be(request.Name);
        response.Status.Should().Be(request.Status);
        response.Priority.Should().Be(request.Priority);
    }

   


    private static RegisterTaskUseCase CreateUseCase(Tasks.Domain.Entities.User usuario)
    {
        var loggedUser = LoggedUsuerBuilder.Instance().GetUser(usuario).Builder();
        var repository = TaskWriteOnlyRepositoryBuilder.Instancia().Construir();
        var unitOfWork = UnitOfWorkBuilder.Instance().Builder();


        return new RegisterTaskUseCase(unitOfWork, loggedUser, repository);
    }
}

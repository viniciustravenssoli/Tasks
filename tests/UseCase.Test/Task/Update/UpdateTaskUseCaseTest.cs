using FluentAssertions;
using Util.Test.LoggedUser;
using Util.Test.Repositories;
using Util.Test.Entities;
using Tasks.Application.UseCases.Task.Update;
using Util.Test.Request;
using Tasks.Execptions.BaseExecptions;
using Tasks.Execptions;

namespace UseCase.Test.Task.Update;
public class UpdateTaskUseCaseTest
{
    [Fact]
    public async System.Threading.Tasks.Task ValidateSuccess()
    {
        (var user, var _) = UserBuilder.Builder();

        var task = TaskBuilder.Builder(user);

        var useCase = CreateUseCase(user, task);

        var request = RequestRegisterTaskBuilder.Builder();

        await useCase.Execute(request, task.Id);

        Func<System.Threading.Tasks.Task> action = async () => { await useCase.Execute(request, task.Id); };

        await action.Should().NotThrowAsync();

        task.Description.Should().Be(request.Description);
        task.Name.Should().Be(request.Name);
        task.Status.Should().Be((Tasks.Domain.Enums.TaskStatus)request.Status);
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateErrorTaskDoNotBelongToUser()
    {
        (var user, var password) = UserBuilder.Builder();
        (var user2, _) = UserBuilder.Builder2();

        var task = TaskBuilder.Builder(user2);

        var useCase = CreateUseCase(user, task);

        var request = RequestRegisterTaskBuilder.Builder();

        Func<System.Threading.Tasks.Task> action = async () => { await useCase.Execute(request, task.Id); };

        await action.Should().ThrowAsync<ValidationErrorException>()
            .Where(exception => exception.ErrorMessages.Count == 1 && exception.ErrorMessages.Contains(ResourceErrorsMessage.TaskNotFound));
    }

    private static UpdateTaskUseCase CreateUseCase(Tasks.Domain.Entities.User usuario, Tasks.Domain.Entities.Task task)
    {
        var usuarioLogado = LoggedUsuerBuilder.Instance().GetUser(usuario).Builder();
        var repositorio = TaskUpdateOnlyRepositoryBuilder.Instancia().GetById(task).Construir();
        var unidadeDeTrabalho = UnitOfWorkBuilder.Instance().Builder();


        return new UpdateTaskUseCase(repositorio, usuarioLogado,unidadeDeTrabalho);
    }
}

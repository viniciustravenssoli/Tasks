using Moq;
using Tasks.Domain.Repositories.Tasks;

namespace Util.Test.Repositories;
public class TaskUpdateOnlyRepositoryBuilder
{
    private static TaskUpdateOnlyRepositoryBuilder _instance;
    private readonly Mock<ITaskUpdateOnlyRepository> _repositorio;

    private TaskUpdateOnlyRepositoryBuilder()
    {
        if (_repositorio is null)
        {
            _repositorio = new Mock<ITaskUpdateOnlyRepository>();
        }
    }

    public static TaskUpdateOnlyRepositoryBuilder Instancia()
    {
        _instance = new TaskUpdateOnlyRepositoryBuilder();
        return _instance;
    }

    public TaskUpdateOnlyRepositoryBuilder GetById(Tasks.Domain.Entities.Task task)
    {
        _repositorio.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);

        return this;
    }

    public ITaskUpdateOnlyRepository Construir()
    {
        return _repositorio.Object;
    }
}

using Moq;
using Tasks.Domain.Repositories.Tasks;

namespace Util.Test.Repositories;
public class TaskWriteOnlyRepositoryBuilder
{
    private static TaskWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<ITaskWriteOnlyRepository> _repository;

    private TaskWriteOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<ITaskWriteOnlyRepository>();
        }
    }

    public static TaskWriteOnlyRepositoryBuilder Instancia()
    {
        _instance = new TaskWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public ITaskWriteOnlyRepository Construir()
    {
        return _repository.Object;
    }
}

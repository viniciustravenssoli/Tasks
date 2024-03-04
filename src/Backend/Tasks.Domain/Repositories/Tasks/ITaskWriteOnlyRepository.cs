namespace Tasks.Domain.Repositories.Tasks;
public interface ITaskWriteOnlyRepository
{
    Task Add(Entities.Task task);
}

namespace Tasks.Domain.Repositories.Tasks;
public interface ITaskUpdateOnlyRepository
{
    void Update(Entities.Task task);
    Task<Entities.Task> GetById(long taskId);
}

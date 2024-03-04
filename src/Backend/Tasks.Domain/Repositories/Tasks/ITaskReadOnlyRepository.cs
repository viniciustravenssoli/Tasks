namespace Tasks.Domain.Repositories.Tasks;
public interface ITaskReadOnlyRepository
{
    Task<IList<Entities.Task>> GetAllFromUser(long userId);
    Task<Entities.Task> GetById(long taskId);
}

namespace Tasks.Domain.Repositories.Tasks;
public interface ITaskReadOnlyRepository
{
    Task<IList<Entities.Task>> GetAllFromUser(long userId);
    Task<Entities.Task> GetById(long taskId);
    Task<(IList<Domain.Entities.Task>, int)> GetAllFromUserWithPagination(long userId, int pageNumber, int pageSize);
}

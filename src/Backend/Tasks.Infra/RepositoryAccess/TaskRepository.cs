using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Repositories.Tasks;

namespace Tasks.Infra.RepositoryAccess;
public class TaskRepository : ITaskReadOnlyRepository, ITaskWriteOnlyRepository
{
    private readonly TaskContext _context;

    public TaskRepository(TaskContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task Add(Domain.Entities.Task task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public async Task<IList<Domain.Entities.Task>> GetAllFromUser(long userId)
    {
        return await _context.Tasks.AsNoTracking()
            .Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Domain.Entities.Task> GetById(long taskId)
    {
        return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == taskId);
    }
}

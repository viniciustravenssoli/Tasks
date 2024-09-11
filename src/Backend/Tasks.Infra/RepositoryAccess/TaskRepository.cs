using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Repositories.Tasks;

namespace Tasks.Infra.RepositoryAccess;
public class TaskRepository : ITaskReadOnlyRepository, ITaskWriteOnlyRepository, ITaskUpdateOnlyRepository
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

    async Task<Domain.Entities.Task> ITaskReadOnlyRepository.GetById(long taskId)
    {
        return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == taskId);
    }

    async Task<Domain.Entities.Task> ITaskUpdateOnlyRepository.GetById(long taskId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
    }

    public void Update(Domain.Entities.Task task)
    {
        _context.Tasks.Update(task);
    }

    public async Task Delete(long taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(r => r.Id == taskId);

        _context.Tasks.Remove(task);
    }

    public async Task<(IList<Domain.Entities.Task>, int)> GetAllFromUserWithPagination(long userId, int pageNumber, int pageSize)
    {
        var query = _context.Tasks.AsQueryable().Where(t => t.UserId == userId);
    
        var totalCount = await query.CountAsync();
    
        var tasks = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    
        return (tasks, totalCount);
    }

}

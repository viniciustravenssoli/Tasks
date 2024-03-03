using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Entities;
using Tasks.Domain.Repositories.User;

namespace Tasks.Infra.RepositoryAccess;
public class UserRepository : IUserReadOnlyRepository, IUserUpdateOnlyRepository, IUserWriteOnlyRepository
{
    private readonly TaskContext _context;

    public UserRepository(TaskContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistUsuarioWithEmail(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email.Equals(email));
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User> GetByEmailAndPassword(string email, string password)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Password.Equals(password));
    }

    public async Task<User> GetById(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}

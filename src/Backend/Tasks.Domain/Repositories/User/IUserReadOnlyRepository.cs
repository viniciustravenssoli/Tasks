namespace Tasks.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistUsuarioWithEmail(string email);
    Task<Entities.User> GetByEmailAndPassword(string email, string password);
    Task<Entities.User> GetByEmail(string email);
}

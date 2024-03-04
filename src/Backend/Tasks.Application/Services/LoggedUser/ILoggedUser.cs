using Tasks.Domain.Entities;

namespace Tasks.Application.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> GetUser();
}

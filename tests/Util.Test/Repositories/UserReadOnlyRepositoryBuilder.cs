using Moq;
using Tasks.Domain.Entities;
using Tasks.Domain.Repositories.User;

namespace Util.Test.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private static UserReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IUserReadOnlyRepository> _repository;

    private UserReadOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }
    }

    public static UserReadOnlyRepositoryBuilder Instance()
    {
        _instance = new UserReadOnlyRepositoryBuilder();
        return _instance;
    }

    public UserReadOnlyRepositoryBuilder ExistUsuarioWithEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repository.Setup(i => i.ExistUsuarioWithEmail(email)).ReturnsAsync(true);

        return this;
    }

    public UserReadOnlyRepositoryBuilder GetByEmailAndPassword(User user)
    {
        _repository.Setup(i => i.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Builder()
    {
        return _repository.Object;
    }
}

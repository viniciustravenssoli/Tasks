using Moq;
using Tasks.Domain.Entities;
using Tasks.Domain.Repositories.User;

namespace Util.Test.Repositories;
public class UserUpdateOnlyRepositoryBuilder
{
    private static UserUpdateOnlyRepositoryBuilder _instance;

    private readonly Mock<IUserUpdateOnlyRepository> _usuarioUpdateOnlyRepository;

    private UserUpdateOnlyRepositoryBuilder()
    {
        if (_usuarioUpdateOnlyRepository == null)
        {
            _usuarioUpdateOnlyRepository = new Mock<IUserUpdateOnlyRepository>();
        }
    }

    public UserUpdateOnlyRepositoryBuilder GetById(User user)
    {
        _usuarioUpdateOnlyRepository.Setup(c => c.GetById(user.Id)).ReturnsAsync(user);
        return this;
    }

    public static UserUpdateOnlyRepositoryBuilder Instance()
    {
        _instance = new UserUpdateOnlyRepositoryBuilder();
        return _instance;
    }

    public IUserUpdateOnlyRepository Builder()
    {
        return _usuarioUpdateOnlyRepository.Object;
    }
}

using Moq;
using Tasks.Domain.Repositories.User;

namespace Util.Test.Repositories;
public class UserWriteOnlyRepositoryBuilder
{
    private static UserWriteOnlyRepositoryBuilder _instance;

    private readonly Mock<IUserWriteOnlyRepository> _usuarioReadOnlyRepository;

    private UserWriteOnlyRepositoryBuilder()
    {
        if (_usuarioReadOnlyRepository == null)
        {
            _usuarioReadOnlyRepository = new Mock<IUserWriteOnlyRepository>();
        }
    }

    public static UserWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new UserWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IUserWriteOnlyRepository Builder()
    {
        return _usuarioReadOnlyRepository.Object;
    }
}

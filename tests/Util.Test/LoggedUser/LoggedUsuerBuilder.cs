using Moq;
using Tasks.Application.Services.LoggedUser;
using Tasks.Domain.Entities;

namespace Util.Test.LoggedUser;
public class LoggedUsuerBuilder
{
    private static LoggedUsuerBuilder _instance;

    private readonly Mock<ILoggedUser> _loggedUser;

    private LoggedUsuerBuilder()
    {
        if (_loggedUser == null)
        {
            _loggedUser = new Mock<ILoggedUser>();
        }
    }

    public LoggedUsuerBuilder GetUser(User user)
    {
        _loggedUser.Setup(c => c.GetUser()).ReturnsAsync(user);
        return this;
    }

    public static LoggedUsuerBuilder Instance()
    {
        _instance = new LoggedUsuerBuilder();
        return _instance;
    }

    public ILoggedUser Builder()
    {
        return _loggedUser.Object;
    }
}

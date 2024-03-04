using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.Application.UseCases.User.Login;
public interface ILoginUseCase
{
    Task<ResponseLogin> Execute(RequestLogin request);
}

using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUser> Execute(RequestRegisterUser request);
}

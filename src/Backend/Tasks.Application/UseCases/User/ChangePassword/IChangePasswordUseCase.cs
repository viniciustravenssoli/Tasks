using Tasks.Communication.Request;

namespace Tasks.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUseCase
{
    System.Threading.Tasks.Task Executar(RequestChangePassword request);
}

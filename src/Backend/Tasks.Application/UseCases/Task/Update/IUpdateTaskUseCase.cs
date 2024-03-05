using Tasks.Communication.Request;

namespace Tasks.Application.UseCases.Task.Update;
public interface IUpdateTaskUseCase
{
    System.Threading.Tasks.Task Execute(RequestRegisterTask request, long id);
}

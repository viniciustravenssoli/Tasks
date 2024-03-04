using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.Application.UseCases.Task.Register;
public interface IRegisterTaskUseCase
{
    Task<ResponseRegisterTask> Execute(RequestRegisterTask request);
}

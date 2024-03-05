using Tasks.Communication.Request;
using Tasks.Communication.Response;

namespace Tasks.Application.UseCases.Task.GetAllFromUser;
public interface IGetAllFromUser
{
    Task<ResponseTaskAllFromUser> Execute(RequestTasks request);
}

using Tasks.Application.Services.LoggedUser;
using Tasks.Communication.Request;
using Tasks.Communication.Response;
using Tasks.Domain.Extensions;
using Tasks.Domain.Repositories.Tasks;

namespace Tasks.Application.UseCases.Task.GetAllFromUser;
public class GetAllFromUser : IGetAllFromUser
{
    private readonly ITaskReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GetAllFromUser(ITaskReadOnlyRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseTaskAllFromUser> Execute(RequestTasks request)
    {
        var loggedUser = await _loggedUser.GetUser();

        var tasks = await _repository.GetAllFromUser(loggedUser.Id);

        tasks = Filter(request, tasks);

        var responseTasks = tasks.Select(x => new ResponseTasks
        {
            Description = x.Description,
            Name = x.Name,
            Status = (Communication.Enums.TaskStatus)x.Status
        }).ToList();

        return new ResponseTaskAllFromUser
        {
            Tasks = responseTasks
        };
    }

    private static IList<Domain.Entities.Task> Filter(RequestTasks request, IList<Domain.Entities.Task> tasks)
    {
        var filtredTasks = tasks;

        if (request.Status.HasValue)
        {
            filtredTasks = tasks.Where(x => x.Status == (Domain.Enums.TaskStatus)request.Status.Value).ToList();
        }

        if (!string.IsNullOrEmpty(request.NameOfTask))
        {
            filtredTasks = tasks.Where(x => x.Name.CompareWithoutConsideringSpecialCharactersAndUpperCase(request.NameOfTask)).ToList();
        }

        return filtredTasks.OrderBy(x => x.Name).ToList();
    }
}

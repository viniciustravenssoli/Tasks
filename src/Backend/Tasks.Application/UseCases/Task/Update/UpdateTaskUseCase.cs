using Tasks.Application.Services.LoggedUser;
using Tasks.Communication.Request;
using Tasks.Domain.Repositories.Tasks;
using Tasks.Execptions.BaseExecptions;
using Tasks.Execptions;
using Tasks.Infra;
using Tasks.Application.UseCases.Task.Register;

namespace Tasks.Application.UseCases.Task.Update;
public class UpdateTaskUseCase : IUpdateTaskUseCase
{
    private readonly ITaskUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskUseCase(ITaskUpdateOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async System.Threading.Tasks.Task Execute(RequestRegisterTask request, long id)
    {
        var loggedUser = await _loggedUser.GetUser();
        var myTask = await _repository.GetById(id);

        Validate(loggedUser, myTask, request);

        UpdateTaskProperties(request, myTask);

        _repository.Update(myTask);
        await _unitOfWork.Commit();
    }

    private void UpdateTaskProperties(RequestRegisterTask request, Domain.Entities.Task myTask)
    {
        myTask.Description = request.Description;
        myTask.Name = request.Name;

        UpdateStatus(request.Status, myTask);
    }

    private void UpdateStatus(Communication.Enums.TaskStatus requestStatus, Domain.Entities.Task myTask)
    {
        switch (requestStatus)
        {
            case Communication.Enums.TaskStatus.NotStarted:
                myTask.Status = Domain.Enums.TaskStatus.NotStarted; 
                break;
            case Communication.Enums.TaskStatus.InProgress:
                myTask.Status = Domain.Enums.TaskStatus.InProgress;
                myTask.StartedAt = DateTime.UtcNow;
                break;
            case Communication.Enums.TaskStatus.Completed:
                myTask.Status = Domain.Enums.TaskStatus.Completed;
                myTask.EndedAt = DateTime.UtcNow;
                break;
        }
    }

    public static void Validate(Domain.Entities.User loggedUser, Domain.Entities.Task task, RequestRegisterTask request)
    {
        if (task == null || task.UserId != loggedUser.Id)
        {
            throw new ValidationErrorException(new List<string> { ResourceErrorsMessage.TaskNotFound });
        }

        var validator = new RegisterTaskValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(errors);
        }
    }
}

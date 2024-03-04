using Tasks.Application.Services.LoggedUser;
using Tasks.Communication.Request;
using Tasks.Communication.Response;
using Tasks.Domain.Repositories.Tasks;
using Tasks.Execptions.BaseExecptions;
using Tasks.Infra;

namespace Tasks.Application.UseCases.Task.Register;
public class RegisterTaskUseCase : IRegisterTaskUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly ITaskWriteOnlyRepository _taskWriteOnlyRepository;

    public RegisterTaskUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser, ITaskWriteOnlyRepository taskWriteOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _taskWriteOnlyRepository = taskWriteOnlyRepository;
    }

    public async Task<ResponseRegisterTask> Execute(RequestRegisterTask request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.GetUser();

        var task = new Domain.Entities.Task
        {
            Name = request.Name,
            StartedAt = null,
            EndedAt = null,
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            Status = (Domain.Enums.TaskStatus)request.Status,
            UserId = (int)loggedUser.Id,
        };

        await _taskWriteOnlyRepository.Add(task);
        await _unitOfWork.Commit();

        var response = new ResponseRegisterTask
        {
            Description = task.Description,
            StartedAt = task.StartedAt,
            EndedAt = task.EndedAt,
            Name = task.Name,
            Status = (Communication.Enums.TaskStatus)task.Status,
        };

        return response;
    }

    private async System.Threading.Tasks.Task Validate(RequestRegisterTask request)
    {
        var validator = new RegisterTaskValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(errors);
        }
    }
}

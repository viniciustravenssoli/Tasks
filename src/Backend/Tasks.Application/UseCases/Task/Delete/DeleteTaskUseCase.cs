using Tasks.Application.Services.LoggedUser;
using Tasks.Domain.Repositories.Tasks;
using Tasks.Execptions;
using Tasks.Execptions.BaseExecptions;
using Tasks.Infra;

namespace Tasks.Application.UseCases.Task.Delete;
public class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITaskWriteOnlyRepository _repositorioWriteOnly;
    private readonly ITaskReadOnlyRepository _repositorioReadOnly;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskUseCase(ITaskWriteOnlyRepository repositorioWriteOnly, ITaskReadOnlyRepository repositorioReadOnly, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
    {
        _repositorioWriteOnly = repositorioWriteOnly;
        _repositorioReadOnly = repositorioReadOnly;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async System.Threading.Tasks.Task Executar(long id)
    {
        var loggedUser = await _loggedUser.GetUser();

        var task = await _repositorioReadOnly.GetById(id);

        Validate(loggedUser, task);

        await _repositorioWriteOnly.Delete(id);

        await _unitOfWork.Commit();
    }

    public static void Validate(Domain.Entities.User loggedUser, Domain.Entities.Task task)
    {
        if (task is null || task.UserId != loggedUser.Id)
        {
            throw new ValidationErrorException(new List<string> { ResourceErrorsMessage.TaskNotFound });
        }
    }
}

namespace Tasks.Application.UseCases.Task.Delete;
public interface IDeleteTaskUseCase
{
    System.Threading.Tasks.Task Executar(long id);
}

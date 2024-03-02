namespace Tasks.Infra;
public interface IUnitOfWork
{
   Task Commit();
}

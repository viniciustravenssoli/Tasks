
namespace Tasks.Communication.Request;

public class RequestTasks
{
    public string? NameOfTask { get; set; }
    public Tasks.Communication.Enums.TaskStatus? Status { get; set; }
    public int PageNumber { get; set; } = 1; // Página padrão = 1
    public int PageSize { get; set; } = 10; // Tamanho padrão da página = 10
}

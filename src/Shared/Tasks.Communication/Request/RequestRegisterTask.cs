namespace Tasks.Communication.Request;
public class RequestRegisterTask
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Enums.TaskStatus Status { get; set; }
    public Enums.TaskPriority? Priority { get; set; }
}

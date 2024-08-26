namespace Tasks.Communication.Response;
public class ResponseTaskAllFromUser
{
    public List<ResponseTasks> Tasks { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
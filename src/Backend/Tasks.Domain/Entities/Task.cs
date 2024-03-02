namespace Tasks.Domain.Entities;
public class Task
{
    public int TaskId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public TaskStatus Status { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

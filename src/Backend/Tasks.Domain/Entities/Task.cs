
namespace Tasks.Domain.Entities;
public class Task : Base
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public Enums.TaskStatus Status { get; set; }
    public long UserId { get; set; }
}

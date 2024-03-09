using Bogus;

namespace Util.Test.Entities;
public class TaskBuilder
{
    public static Tasks.Domain.Entities.Task Builder(Tasks.Domain.Entities.User user)
    {
        return new Faker<Tasks.Domain.Entities.Task>()
            .RuleFor(c => c.Id, _ => user.Id)
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Status, f => f.PickRandom<Tasks.Domain.Enums.TaskStatus>())
            .RuleFor(c => c.EndedAt, f => f.Date.Future())
            .RuleFor(c => c.Name, f => f.Random.Word())
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.UserId, _ => user.Id);
    }
}

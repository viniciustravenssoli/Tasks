using Bogus;
using Tasks.Communication.Request;
using Tasks.Domain.Enums;

namespace Util.Test.Request;
public class RequestRegisterTaskBuilder
{
    public static RequestRegisterTask Builder()
    {
        return new Faker<RequestRegisterTask>()
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Status, f => f.PickRandom<Tasks.Communication.Enums.TaskStatus>())
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.Priority, f => f.PickRandom<Tasks.Communication.Enums.TaskPriority>());
    }
}

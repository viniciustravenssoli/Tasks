using Bogus;
using Tasks.Communication.Request;

namespace Util.Test.Request;
public class RequestChangePasswordBuilder
{
    public static RequestChangePassword Builder(int passwordLength = 10)
    {
        return new Faker<RequestChangePassword>()
               .RuleFor(c => c.CurrentPassword, f => f.Internet.Password(10))
               .RuleFor(c => c.NewPassword, f => f.Internet.Password(passwordLength));
    }
}

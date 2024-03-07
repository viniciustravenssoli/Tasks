using Bogus;
using Tasks.Communication.Request;

namespace Util.Test.Request;
public class RequestRegisterUserBuilder
{
    public static RequestRegisterUser Builder(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUser>()
            .RuleFor(c => c.UserName, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Password, f => f.Internet.Password(passwordLength))
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"));
    }
}

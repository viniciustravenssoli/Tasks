using Bogus;
using Tasks.Domain.Entities;
using Util.Test.Encrypt;

namespace Util.Test.Entities;
public class UserBuilder
{
    public static (User user, string password) Builder()
    {
        string password = string.Empty;

        var generatedUser = new Faker<User>()
            .RuleFor(c => c.Id, _ => 1)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Password, f =>
            {
                password = f.Internet.Password();
                return PasswordEncryptBuilder.Instance().Criptograph(password);

            })
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"));

        return (generatedUser, password);
    }

    public static (User usuario, string senha) Builder2()
    {
        (var usuario, var senha) = Builder();
        usuario.Id = 2;

        return (usuario, senha);
    }

}

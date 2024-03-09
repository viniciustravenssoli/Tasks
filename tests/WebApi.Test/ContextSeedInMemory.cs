using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Infra.RepositoryAccess;
using Util.Test.Entities;

namespace WebApi.Test;
public class ContextSeedInMemory
{
    public static (Tasks.Domain.Entities.User user, string password) Seed(TaskContext context)
    {
        (var user, string password) = UserBuilder.Builder();

        context.Users.Add(user);

        context.SaveChanges();

        return (user, password);
    }
}

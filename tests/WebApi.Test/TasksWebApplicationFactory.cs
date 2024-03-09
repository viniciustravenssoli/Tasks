using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Domain.Entities;
using Tasks.Infra.RepositoryAccess;

namespace WebApi.Test;
public class TasksWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : class
{
    private User user;
    private string password;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(TaskContext));

                if (descritor != null)
                    services.Remove(descritor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<TaskContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });
                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;

                var database = scopeService.GetRequiredService<TaskContext>();
                database.Database.EnsureDeleted();

                (user, password) = ContextSeedInMemory.Seed(database);
            });
    }

    public User GetUsuario()
    {
        return user;
    }

    public string GetPassword()
    {
        return password;
    }
}

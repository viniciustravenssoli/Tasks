using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tasks.Domain.Extensions;
using Tasks.Domain.Repositories.Tasks;
using Tasks.Domain.Repositories.User;
using Tasks.Infra.RepositoryAccess;

namespace Tasks.Infra;
public static class BootStrapper
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddRepositories(services);
        AddUnitOfWork(services);
        AddContext(services, configurationManager);
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {

        bool.TryParse(configurationManager.GetSection("Configurations:DbInMemory").Value, out bool dbInMemory);

        if (!dbInMemory)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 20));
            var connectionString = configurationManager.GetFullConnection();

            services.AddDbContext<TaskContext>(options =>
            {
                options.UseMySql(connectionString, serverVersion);
            });
        }
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        bool.TryParse(configurationManager.GetSection("Configurations:DbInMemory").Value, out bool dbInMemory);

        if (!dbInMemory)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddMySql5()
            .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("Tasks.Infra")).For.All()
            );
        }
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<ITaskReadOnlyRepository, TaskRepository>();
        services.AddScoped<ITaskWriteOnlyRepository, TaskRepository>();
        services.AddScoped<ITaskUpdateOnlyRepository, TaskRepository>();
    }
}

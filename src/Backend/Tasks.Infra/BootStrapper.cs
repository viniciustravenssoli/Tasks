using FluentMigrator.Runner;
using Tasks.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tasks.Infra.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Repositories.User;

namespace Tasks.Infra;
public static class BootStrapper
{


    public static void AddInfra(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddRepositories(services);
        AddUnitOfWork(services);
        var connection2 = configurationManager.GetConnection2();

        services.AddDbContext<TaskContext>(
        options => options.UseMySQL(connection2));

    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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
        
    }
}

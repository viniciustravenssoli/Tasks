using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Services.Cryptograpy;
using Tasks.Application.Services.LoggedUser;
using Tasks.Application.Services.Token;
using Tasks.Application.UseCases.Task.Register;
using Tasks.Application.UseCases.User.Login;
using Tasks.Application.UseCases.User.Register;

namespace Tasks.Application;
public static class BootStrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddJwtToken(services, configuration);
        AddOptionalPassworKey(services, configuration);
        AddUseCases(services);
        AddLoggedUser(services);
    }
    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddOptionalPassworKey(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configurations:Key");

        services.AddScoped(option => new PasswordEncryption(section.Value));
    }

    private static void AddJwtToken(IServiceCollection services, IConfiguration configuration)
    {
        var sectionKey = configuration.GetRequiredSection("Configurations:TokenKey");
        var sectionTimeExpired = configuration.GetRequiredSection("Configurations:TokenTimeExpired");

        services.AddScoped(options => new TokenController(int.Parse(sectionTimeExpired.Value), sectionKey.Value));
    }
    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IRegisterTaskUseCase, RegisterTaskUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
    }
}

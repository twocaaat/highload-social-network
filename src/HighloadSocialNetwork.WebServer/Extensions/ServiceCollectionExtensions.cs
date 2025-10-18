using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Repositories;
using HighloadSocialNetwork.WebServer.Mappers;
using HighloadSocialNetwork.WebServer.Mappers.Interafaces;
using HighloadSocialNetwork.WebServer.Services;
using HighloadSocialNetwork.WebServer.Services.Interfaces;

namespace HighloadSocialNetwork.WebServer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddCustomServices();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAuthRepository, AuthRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IFakeRepository, FakeRepository>();
    }
    
    private static void AddCustomServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IModelsMapper, ModelsMapper>();
        services.AddSingleton<IContractsMapper, ContractsMapper>();
    }
}
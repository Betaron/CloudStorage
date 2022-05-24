using CloudStorage.Core.Domains.Storages.Services;
using CloudStorage.Core.Domains.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Core;

public static class Bootstraps
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStorageService, StorageService>();

        return services;
    }
}


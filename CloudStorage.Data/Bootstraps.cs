using CloudStorage.Core;
using CloudStorage.Core.Domains.Storages.Repositories;
using CloudStorage.Core.Domains.Users.Repositories;
using CloudStorage.Data.Storages.Repositories;
using CloudStorage.Data.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Data;

public static class Bootstraps
{
    public static IServiceCollection AddData(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CloudStorageContext>(options =>
        {
            options.UseNpgsql(configuration["Databases:PostgreSql"]);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStorageRepository, StorageRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

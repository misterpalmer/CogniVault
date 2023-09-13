using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityEFCoreProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("IdentityDbConnection"));
        });

        return services;
    }

    public static IServiceProvider MigrateIdentityEFCoreProvider<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.Migrate();

        return serviceProvider;
    }
}
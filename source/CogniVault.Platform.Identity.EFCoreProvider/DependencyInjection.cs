using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Abstractions.Persistence.EFCore;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.EFCoreProvider.Services;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityEFCoreProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbResolver, IdentityDbResolver>();
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("IdentityDbConnection"));
        });

        services.AddScoped<IdentityContext>();
        services.AddScoped<DbContext, IdentityContext>();
        
        return services;
    }

    public static IServiceProvider MigrateIdentityEFCoreProvider<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.Migrate();

        return serviceProvider;
    }

    public static IServiceCollection AddEFCoreRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlatformOrganizationService, OrganizationService>();

        // // ... Repeat for other entities as necessary
        // services.AddSingleton<IUnitOfWork, IdentityInMemoryUnitOfWork>();
        // services.AddSingleton<IPlatformUserService, UserService>();
        // services.AddSingleton<IPlatformOrganizationService, OrganizationService>();

        return services;
    }

    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        // services.AddSingleton<IRepositoryAsync<PlatformUser>, InMemoryRepositoryAsync<PlatformUser>>();
        // services.AddSingleton<IRepositoryAsync<PlatformOrganization>, InMemoryRepositoryAsync<PlatformOrganization>>();

        // // ... Repeat for other entities as necessary
        // services.AddSingleton<IUnitOfWork, IdentityInMemoryUnitOfWork>();
        // services.AddSingleton<IPlatformUserService, UserService>();
        // services.AddScoped<IDbResolver, IdentityDbResolver>();
        // services.AddScoped<IdentityContext>();
        // services.AddScoped<IPlatformOrganizationService, OrganizationService>();

        return services;
    }
}
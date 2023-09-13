using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;
using CogniVault.Platform.Identity.InMemoryProvider.Services;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Stores;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity.InMemoryProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddTransient<IValidator<OrganizationName>, OrganizationNameValidator>();
        services.AddTransient<IValidator<TenantName>, TenantNameValidator>();
        services.AddTransient<IValidator<InterfaceName>, InterfaceNameValidator>();
        services.AddTransient<IValidator<Username>, UsernameValidator>();
        services.AddTransient<IValidator<PlainPassword>, PlainPasswordValidator>();

        services.AddSingleton<IUserTokenStore<Guid>, UserTokenInMemoryStore<Guid>>();
        services.AddSingleton<IRolePermissionStore<PlatformUser, Guid>, RolePermissionInMemoryStore<PlatformUser, Guid>>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        

        // Registering the InMemoryRepository for specific entities.
        services.AddSingleton<IQueryRepositoryAsync<PlatformOrganization, Guid>, InMemoryRepositoryAsync<PlatformOrganization, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformOrganization, Guid>, InMemoryRepositoryAsync<PlatformOrganization, Guid>>();

        services.AddSingleton<IQueryRepositoryAsync<PlatformTenant, Guid>, InMemoryRepositoryAsync<PlatformTenant, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformTenant, Guid>, InMemoryRepositoryAsync<PlatformTenant, Guid>>();

        services.AddSingleton<IQueryRepositoryAsync<PlatformInterface, Guid>, InMemoryRepositoryAsync<PlatformInterface, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformInterface, Guid>, InMemoryRepositoryAsync<PlatformInterface, Guid>>();

        // ... Repeat for other entities as necessary
        services.AddSingleton<IUnitOfWork, IdentityInMemoryUnitOfWork>();
        services.AddSingleton<IPlatformUserService, UserService>();
        services.AddSingleton<IPlatformOrganizationService, OrganizationService>();

        return services;
    }
}


﻿using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;
using CogniVault.Platform.Identity.InMemoryProvider.Services;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity.InMemoryProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        // Other registrations
        // services.AddSingleton<IPlatformOrganizationRepository, OrganizationRepository>();
        // services.AddSingleton<IPlatformTenantRepository, TenantRepository>();
        // services.AddSingleton<IPlatformInterfaceRepository, InterfaceRepository>();

        services.AddTransient<IValidator<OrganizationName>, OrganizationNameValidator>();


        // Registering the InMemoryRepository for specific entities.
        services.AddSingleton<IQueryRepositoryAsync<PlatformOrganization, Guid>, InMemoryRepositoryAsync<PlatformOrganization, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformOrganization, Guid>, InMemoryRepositoryAsync<PlatformOrganization, Guid>>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformOrganization, Guid>, OrganizationRepository>();

        services.AddSingleton<IQueryRepositoryAsync<PlatformTenant, Guid>, InMemoryRepositoryAsync<PlatformTenant, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformTenant, Guid>, InMemoryRepositoryAsync<PlatformTenant, Guid>>();

        services.AddSingleton<IQueryRepositoryAsync<PlatformInterface, Guid>, InMemoryRepositoryAsync<PlatformInterface, Guid>>();
        services.AddSingleton<ICommandRepositoryAsync<PlatformInterface, Guid>, InMemoryRepositoryAsync<PlatformInterface, Guid>>();

        // ... Repeat for other entities as necessary
        services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
        services.AddSingleton<IPlatformOrganizationService, OrganizationService>();

        return services;
    }
}

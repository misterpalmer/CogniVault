using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity;
public static class DependencyInjection
{
    public static IServiceCollection AddIdentityValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<OrganizationName>, OrganizationValidator>();
        services.AddTransient<IValidator<TenantName>, TenantValidator>();
        services.AddTransient<IValidator<InterfaceName>, InterfaceValidator>();
        services.AddTransient<IValidator<Username>, UsernameValidator>();
        services.AddTransient<IValidator<Email>, EmailValidator>();
        services.AddTransient<IValidator<Quota>, QuotaValidator>();
        services.AddTransient<IValidator<PlainPassword>, PlainPasswordValidator>();
        services.AddSingleton<IPasswordEncryptor, PasswordEncryptor>();

        return services;
    }
}

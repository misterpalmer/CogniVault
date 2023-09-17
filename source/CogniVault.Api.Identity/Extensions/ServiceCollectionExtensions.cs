using CogniVault.Platform.Core.RestApi.Configuration;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Services;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;


namespace CogniVault.Api.Identity.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "CogniVault.Api.Identity");
            });
        });

        return services;
    }

    public static IServiceCollection AddSomeMoreRepositories(this IServiceCollection services)
    {
        // services.AddSingleton<IPlatformUserRepository<PlatformUser>, UserRepository>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformUser, Guid>>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformOrganization, Guid>>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformTenant, Guid>>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformInterface, Guid>>();

        return services;
    }

    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddSingleton<IPlatformUserRepository<PlatformUser>, UserRepository>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformUser, Guid>>();
        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddTransient<TokenService>();
        services.AddTransient<LoginService>();

        return services;
    }
}



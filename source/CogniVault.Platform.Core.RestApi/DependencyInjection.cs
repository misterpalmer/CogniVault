using CogniVault.Platform.Core.RestApi.Abstractions;
using CogniVault.Platform.Core.RestApi.Exceptions;
using CogniVault.Platform.Core.RestApi.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Core.RestApi;

public static class DependencyInjection
{
    public static IServiceCollection AddRestApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddTransient<IExceptionHandler, ValidationExceptionHandler>();
        services.AddTransient<IExceptionHandler, GenericExceptionHandler>();

        return services;
    }
}
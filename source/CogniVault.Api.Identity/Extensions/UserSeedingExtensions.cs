using System.Text.Json;

using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.RestApi.Configuration;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Repositories;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

using Microsoft.Extensions.Options;


namespace CogniVault.Api.Identity.Extensions;
public static class RepositoryExtensions
{
    public static IServiceCollection SeedPlatformUsers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPlatformUserRepository<PlatformUser>, PlatformUserRepositoryInMemory>();
        services.AddSingleton<InMemoryRepositoryAsync<PlatformUser, Guid>>();

        var jwtOptions = new JwtOptions();
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IPasswordEncryptor, PasswordEncryptor>();
        services.AddSingleton<IValidator<Email>, EmailValidator>();
        services.AddSingleton<IValidator<PlainPassword>, PlainPasswordValidator>();
        services.AddSingleton<IValidator<Quota>, QuotaValidator>();
        services.AddSingleton<IValidator<Username>, UsernameValidator>();

        // // Register a hosted service that will run when the application starts.
        services.AddHostedService<PlatformUserSeeder>();

        return services;
    }
}

public class PlatformUserSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public PlatformUserSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IPlatformUserRepository<PlatformUser>>();
        var usernameValidator = scope.ServiceProvider.GetRequiredService<IValidator<Username>>();
        var plainPasswordValidator = scope.ServiceProvider.GetRequiredService<IValidator<PlainPassword>>();
        var passwordEncryptor = scope.ServiceProvider.GetRequiredService<IPasswordEncryptor>();
        var emailValidator = scope.ServiceProvider.GetRequiredService<IValidator<Email>>();
        var quotaValidator = scope.ServiceProvider.GetRequiredService<IValidator<Quota>>();

        // try
        // {
        //     var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
        //     Console.WriteLine($"Inside StartAsyncScope JwtOptions: {JsonSerializer.Serialize(jwtOptions.Value)}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"An error occurred: {ex.Message}");
        // }

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        SeedUsers(unitOfWork, passwordEncryptor, usernameValidator, plainPasswordValidator, emailValidator, quotaValidator);
        return Task.CompletedTask;
    }

    private async void SeedUsers(IUnitOfWork unitOfWork, IPasswordEncryptor passwordEncryptor, IValidator<Username> usernameValidator, IValidator<PlainPassword> plainPasswordValidator, IValidator<Email> emailValidator, IValidator<Quota> quotaValidator)
    {
        var username = await Username.CreateAsync("sampleUser", usernameValidator);
        var plainPassword = await PlainPassword.CreateAsync("samplePassword.23", plainPasswordValidator);
        var encryptedPassword = await EncryptedPassword.CreateAsync(plainPassword, passwordEncryptor);
        var email = await Email.CreateAsync("sampleUser@example.com", emailValidator);
        var quota = await Quota.CreateAsync(1000, quotaValidator);
        var timeZone = TimeZoneInfo.Local;
        var currentDateTime = DateTimeOffset.UtcNow;

        var user = new PlatformUser(username, encryptedPassword, email, quota, timeZone, currentDateTime);
        unitOfWork.CommandRepository<PlatformUser, Guid>().InsertAsync(user).Wait();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}



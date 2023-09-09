using CogniVault.Api.FileManager.HostedServices;
using CogniVault.FileSystem.Provider.Memory.Repositories;

namespace CogniVault.Api.FileManager.Extensions;

public static class FileSystemSeedingExtensions
{
    public static IServiceCollection SeedFileSystem(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddSingleton<IFileSystemRepository, FileSystemRepository>();
        // services.AddSingleton<FileSystemInMemoryRepositoryAsync<FileSystem, Guid>>();

        // Register a hosted service that will run when the application starts.
        services.AddHostedService<FileSystemSeeder>();

        return services;
    }
}
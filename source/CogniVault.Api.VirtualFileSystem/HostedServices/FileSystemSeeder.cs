using Bogus;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.Entities;
using CogniVault.Application.VirtualFileSystem.Provider;
using CogniVault.Application.VirtualFileSystem.ValueObjects;
using FluentValidation;


namespace CogniVault.Api.VirtualFileSystem.HostedServices;

public class FileSystemSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbResolver _fileSystemContext;
    private readonly ILogger<FileSystemSeeder> _logger;
    private IVirtualFileSystem _fileSystem;
    private readonly IValidator<DirectoryName> _directoryNameValidator;

    private static readonly Action<ILogger, string, Exception> _startHostedServiceLog;
    private static readonly Action<ILogger, string, Exception> _stopHostedServiceLog;

    static FileSystemSeeder()
    {
        _startHostedServiceLog = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(StartAsync)),
            "Starting hosted service {HostedServiceName}.");
        _stopHostedServiceLog = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2, nameof(StopAsync)),
            "Stopping hosted service {HostedServiceName}.");
    }

    public FileSystemSeeder(IServiceProvider serviceProvider, ILogger<FileSystemSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _fileSystemContext = _serviceProvider.GetRequiredService<IDbResolver>();
        _logger = logger;

        _fileSystem = _serviceProvider.GetRequiredService<IVirtualFileSystem>();
        _directoryNameValidator = _serviceProvider.GetRequiredService<IValidator<DirectoryName>>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _startHostedServiceLog(_logger, nameof(FileSystemSeeder), null);

        await SeedDirectories();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _stopHostedServiceLog(_logger, nameof(FileSystemSeeder), null);
        return Task.CompletedTask;
    }

    public async Task SeedDirectories()
    {
        int rootDirectoryCount = 5;

        var rootDirectories = await GenerateAsync(rootDirectoryCount, async faker =>
        {
            string directoryName = GenerateValidDirectoryNames(faker);
            return await DirectoryName.CreateAsync(directoryName, _directoryNameValidator) ?? DirectoryName.Null;
        });

        var created = await Task.WhenAll(Enumerable.Range(0, rootDirectoryCount).Select(async i =>
        {
            var directory = await _fileSystem.CreateDirectoryAsync(_fileSystem.Root.Id, rootDirectories[i]);
            return directory;
        }));

        await _fileSystemContext.CommandRepository<DirectoryNode>().InsertAsync(created);
    }

    private async Task<List<T>> GenerateAsync<T>(int count, Func<Faker, Task<T>> generatorFunc)
    {
        var faker = new Faker();
        
        var tasks = Enumerable.Range(0, count).Select(_ => generatorFunc(faker));

        return await Task.WhenAll(tasks).ContinueWith(t => t.Result.ToList());
    }

    private string GenerateValidDirectoryNames(Faker faker)
    {
        // Generate a random length between 3 and 255. 
        // 3 is the minimum length to ensure the string starts and ends with a letter or number
        // and has at least one character in between (which can also be a letter, number, or hyphen).
        int length = faker.Random.Int(3, 5);

        // Generate the starting character (must be a letter or number)
        char startChar = faker.Random.String2(1, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")[0];

        // Generate the ending character (must also be a letter or number)
        char endChar = faker.Random.String2(1, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")[0];

        // Generate the middle section (can contain letters, numbers, hyphens)
        string middle = faker.Random.String2(length - 2, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-");

        // Concatenate them all together
        string finalString = startChar + middle + endChar;

        return finalString;
    }
}



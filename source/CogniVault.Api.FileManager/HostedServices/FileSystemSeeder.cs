using System.Text.Json;

using CogniVault.Platform.Core.Abstractions.Identity;
using CogniVault.Platform.Core.RestApi.Configuration;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Api.FileManager.HostedServices;

public class FileSystemSeeder : IHostedService
{
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FileSystemSeeder> _logger;
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

    public FileSystemSeeder(HttpClient httpClient, IServiceProvider serviceProvider, ILogger<FileSystemSeeder> logger)
    {
        _startHostedServiceLog(logger, nameof(FileSystemSeeder), null);

        _httpClient = httpClient;
        // _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("IDENTITY_API_URL") ?? "https://identity:7166");
        _httpClient.BaseAddress = new Uri("https://localhost:7166");
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // using var scope = _serviceProvider.CreateScope();
        var response = await _httpClient.GetAsync("api/organization", cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var currentUserJson = await response.Content.ReadAsStreamAsync(cancellationToken);
            var organizations = await JsonSerializer.DeserializeAsync<IEnumerable<OrganizationNameDto>>(currentUserJson, Common.DefaultJsonSerializerOptions, cancellationToken);
            foreach(var org in organizations)
            {
                var json = JsonSerializer.Serialize(org, Common.DefaultJsonSerializerOptions);
                _logger?.LogInformation(json);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _stopHostedServiceLog(_logger, nameof(FileSystemSeeder), null);
        return Task.CompletedTask;
    }
}

public class OrganizationNameDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}

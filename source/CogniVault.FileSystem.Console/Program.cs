using Microsoft.Extensions.DependencyInjection;
using CogniVault.Application.Abstractions;
using CogniVault.Application.Entities;
using CogniVault.Application;

// Create a new service collection
var services = new ServiceCollection();


services.AddScoped<IFileSystem, FileSystem>(); // replace FileSystemImplementation with your class implementing IFileSystem
services.AddScoped<IFileManager, FileManager>();


// Create a new service provider
using (var serviceProvider = services.BuildServiceProvider())
{
    // Resolve the services from the service provider
    var fileManager = serviceProvider.GetRequiredService<IFileManager>();
    
    // Use your service
    // ...
}
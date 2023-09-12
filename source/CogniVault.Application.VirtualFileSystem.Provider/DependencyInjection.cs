using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.Entities;
using CogniVault.Application.VirtualFileSystem.Validators;
using CogniVault.Application.VirtualFileSystem.ValueObjects;
using CogniVault.Application.VirtualFileSystem.Provider;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CogniVault.Application.VirtualFileSystem.Provider.Memory;
using CogniVault.Application.VirtualFileSystem.Provider.EFCore;

namespace CogniVault.Application.VirtualFileSystem.Provider;

public static class FileSystemRepositoryDependencyInjection
{
    public static IServiceCollection AddVirtualFileSystemMemoryProvider(this IServiceCollection services)
    {
        services.AddTransient<IValidator<DirectoryName>, DirectoryNameValidator>();

        // Registering the InMemoryRepository for specific entities.
        services.AddSingleton<IQueryRepositoryAsync<DirectoryNode>, MemoryRepositoryAsync<DirectoryNode>>();
        services.AddSingleton<ICommandRepositoryAsync<DirectoryNode>, MemoryRepositoryAsync<DirectoryNode>>();
        
        services.AddSingleton<IQueryRepositoryAsync<FileNode>, MemoryRepositoryAsync<FileNode>>();
        services.AddSingleton<ICommandRepositoryAsync<FileNode>, MemoryRepositoryAsync<FileNode>>();

        services.AddSingleton<IQueryRepositoryAsync<SymbolicLinkNode>, MemoryRepositoryAsync<SymbolicLinkNode>>();
        services.AddSingleton<ICommandRepositoryAsync<SymbolicLinkNode>, MemoryRepositoryAsync<SymbolicLinkNode>>();

        // ... Repeat for other entities as necessary
        // services.AddSingleton<IUnitOfWork, MemoryContext>();
        services.AddSingleton<IDbResolver, MemoryDbResolver>();
        services.AddSingleton<IVirtualFileSystem, MemoryFileSystem>();

        return services;
    }

    public static IServiceCollection AddVirtualFileSystemEFCoreProvider(this IServiceCollection services)
    {
        services.AddTransient<IValidator<DirectoryName>, DirectoryNameValidator>();

        // Registering the InMemoryRepository for specific entities.
        services.AddSingleton<IQueryRepositoryAsync<DirectoryNode>, DbContextRepositoryAsync<DirectoryNode>>();
        services.AddSingleton<ICommandRepositoryAsync<DirectoryNode>, DbContextRepositoryAsync<DirectoryNode>>();
        
        services.AddSingleton<IQueryRepositoryAsync<FileNode>, DbContextRepositoryAsync<FileNode>>();
        services.AddSingleton<ICommandRepositoryAsync<FileNode>, DbContextRepositoryAsync<FileNode>>();

        services.AddSingleton<IQueryRepositoryAsync<SymbolicLinkNode>, DbContextRepositoryAsync<SymbolicLinkNode>>();
        services.AddSingleton<ICommandRepositoryAsync<SymbolicLinkNode>, DbContextRepositoryAsync<SymbolicLinkNode>>();

        // ... Repeat for other entities as necessary
        // services.AddSingleton<IUnitOfWork, MemoryContext>();
        services.AddSingleton<IDbResolver, DbContextResolver>();
        services.AddSingleton<IVirtualFileSystem, MemoryFileSystem>();

        return services;
    }
}


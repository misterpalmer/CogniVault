using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.Entities;
using CogniVault.Application.VirtualFileSystem.Provider.Specifications;
using CogniVault.Application.VirtualFileSystem.ValueObjects;
using CogniVault.Platform.Core.Extensions;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Application.VirtualFileSystem.Provider.Memory;

public class MemoryFileSystem : IVirtualFileSystem
{
    public RootNode Root { get; }
    private readonly IDbResolver _dbResolver;

    // protected readonly IUnitOfWork _dbResolver;

    public MemoryFileSystem(IDbResolver dbResolver)
    {
        Root = RootNode.Instance;
        _dbResolver = dbResolver ?? throw new ArgumentNullException(nameof(dbResolver));
    }

    public Task<RootNode> GetRootNodeAsync()
    {
        return Task.FromResult(Root);
    }

    public async Task<DirectoryNode> CreateDirectoryAsync(Guid parent, DirectoryName name)
    {
        // var spec = new GetDirectoryContentsSpecification(parent);
        // var parentDirectory = await _dbResolver.QueryRepository<DirectoryNode>().GetFirstOrDefaultAsync((ISpecification<DirectoryNode>)spec);

        var directoryNode = new DirectoryNode(name, Root);
        await _dbResolver.CommandRepository<DirectoryNode>().InsertAsync(directoryNode);

        // await _dbResolver.CompleteAsync();

        return directoryNode;
    }

    public async Task<FileNode> CreateFileAsync(Guid parent, FileName name)
    {
        var spec = new GetAllFromDirectoryNodeSpecification(parent);
        var parentDirectory = await _dbResolver.QueryRepository<DirectoryNode>().GetFirstOrDefaultAsync((ISpecification<DirectoryNode>)spec);

        var fileNode = new FileNode(name, Root);
        fileNode = new FileNode(name, parentDirectory);
        await _dbResolver.CommandRepository<FileNode>().InsertAsync(fileNode);

        // await _dbResolver.CompleteAsync();

        return fileNode;
    }


    public async Task<IEnumerable<DirectoryNode>> GetDirectoryAsync(Guid parent)
    {
        // Create specific specifications for each type
        var dirSpec = new GetAllFromDirectoryNodeSpecification(parent);

        // Query repositories using these specific specifications
        var dirs = await _dbResolver.QueryRepository<DirectoryNode>().GetAllAsync(dirSpec);

        var results = new List<DirectoryNode>();

        // Add results to the final list
        results.AddRange(await dirs.ToListAsync());

        return results;
    }

    public async Task<IEnumerable<FileNode>> GetFileAsync(Guid parent)
    {
        // Create specific specifications for each type
        var spec = new GetAllFromDirectoryNodeSpecification(parent);
        var parentDirectory = await _dbResolver.QueryRepository<DirectoryNode>().GetFirstOrDefaultAsync((ISpecification<DirectoryNode>)spec);

        var fileSpec = new GetAllFromFileNodeSpecification(parentDirectory.Id);
        // Query repositories using these specific specifications
        var files = await _dbResolver.QueryRepository<FileNode>().GetAllAsync(fileSpec);

        var results = new List<FileNode>();

        // Add results to the final list
        results.AddRange(await files.ToListAsync());

        return results;
    }


    public Task<T> ReadAsync<T>(IFileSystemNode resource)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync<T>(IFileSystemNode resource, T data)
    {
        throw new NotImplementedException();
    }

    public async Task<FileSystemNode> GetNodeAsync(Guid id)
    {
        if (id == Guid.Empty)
            return await Task.FromResult<FileSystemNode>(Root);

        var spec = new GetDirectoryContentsSpecification<DirectoryNode>(id);
        var dirs = await _dbResolver.QueryRepository<DirectoryNode>().GetAllAsync(spec);
        // var files = await _dbResolver.QueryRepository<FileNode>().GetAllAsync((ISpecification<FileNode>)spec);
        // var links = await _dbResolver.QueryRepository<SymbolicLinkNode>().GetAllAsync((ISpecification<SymbolicLinkNode>)spec);

        var results = new List<FileSystemNode>();
        results.AddRange(await dirs.ToListAsync());
        // results.AddRange(await files.ToListAsync());
        // results.AddRange(await links.ToListAsync());

        return results.First();

    }

    public Task DeleteNodeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task MoveNodeAsync(Guid id, IFileSystemNode newParent)
    {
        throw new NotImplementedException();
    }

    public Task LockResourceAsync(IFileSystemNode resource)
    {
        throw new NotImplementedException();
    }

    public Task UnlockResourceAsync(IFileSystemNode resource)
    {
        throw new NotImplementedException();
    }
}
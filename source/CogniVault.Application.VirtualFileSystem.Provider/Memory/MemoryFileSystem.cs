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


    public async Task<IEnumerable<FileSystemNode>> GetDirectoryAsync(Guid parent)
{
    // Create specific specifications for each type
    var dirSpec = new GetAllFromDirectoryNodeSpecification(parent);
    var fileSpec = new GetAllFromFileNodeSpecification(parent);
    var linkSpec = new GetAllFromSymbolicLinkNodeSpecification(parent);

    // Query repositories using these specific specifications
    var dirs = await _dbResolver.QueryRepository<DirectoryNode>().GetAllAsync(dirSpec);
    var files = await _dbResolver.QueryRepository<FileNode>().GetAllAsync(fileSpec);
    var links = await _dbResolver.QueryRepository<SymbolicLinkNode>().GetAllAsync(linkSpec);

    var results = new List<FileSystemNode>();

    // Add results to the final list
    results.AddRange(await dirs.ToListAsync());
    results.AddRange(await files.ToListAsync());
    results.AddRange(await links.ToListAsync());

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
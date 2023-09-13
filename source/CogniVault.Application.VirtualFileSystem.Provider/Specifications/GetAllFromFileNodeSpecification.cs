using CogniVault.Application.VirtualFileSystem.Entities;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetAllFromFileNodeSpecification : GetAllFromNodeSpecification<FileNode>
{
    public GetAllFromFileNodeSpecification(Guid parentId) : base(parentId) { }
}
using CogniVault.Application.VirtualFileSystem.Entities;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetAllFromSymbolicLinkNodeSpecification : GetAllFromNodeSpecification<SymbolicLinkNode>
{
    public GetAllFromSymbolicLinkNodeSpecification(Guid parentId) : base(parentId) { }
}
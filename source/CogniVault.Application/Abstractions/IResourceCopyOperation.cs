namespace CogniVault.Application.Abstractions;
public interface IResourceCopyOperation : IResourceOperation
{
    IResource CopyTo(IResource target, bool overwrite);
    IResource CopyToDirectory(IDirectory target, bool overwrite);
}
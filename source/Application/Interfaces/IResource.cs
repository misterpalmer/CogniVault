using CogniVault.Application.ValueObjects;


namespace CogniVault.Application.Interfaces;

public interface IResource
{
    ResourceName Name { get; set; }
    IPermission Permissions { get; set; }
}
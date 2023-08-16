using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemOperation
{
    OperationName Name { get; set; }
}

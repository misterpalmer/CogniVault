namespace CogniVault.Application.Exceptions;

public class FileSystemNotFoundException : Exception
{
    public FileSystemNotFoundException(string typeName)
        : base($"A FileSystem of type {typeName} could not be found.")
    {
    }
}
namespace CogniVault.Application.Exceptions;

public class FileSystemCreationException : Exception
{
    public FileSystemCreationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
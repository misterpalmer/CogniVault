namespace CogniVault.Application.Entities;

public class FileSystemPath : IEquatable<FileSystemPath>, IComparable<FileSystemPath>
{
    public string Path => _path ?? "/";
    private readonly string? _path;

    public FileSystemPath(string path)
    {
        _path = path;
    }

    public bool Equals(FileSystemPath? other)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(FileSystemPath? other)
    {
        throw new NotImplementedException();
    }
}
namespace CogniVault.Platform.Identity;
public class UserManagement : IUserManagement
{
    private FileSystemUser _user;
    private ITimeProvider _timeProvider;

    public UserManagement(FileSystemUser user, ITimeProvider timeProvider)
    {
        _user = user;
        _timeProvider = timeProvider;
    }
    // implement IUserManagement methods
}

public interface IUserManagement
{
}
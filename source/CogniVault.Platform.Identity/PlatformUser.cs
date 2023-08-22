using CogniVault.Platform.Core.Abstractions.Identity;

namespace CogniVault.Platform.Identity;

public class PlatformUser : ICurrentUser
{
    public bool IsAuthenticated => throw new NotImplementedException();

    public string UserName => throw new NotImplementedException();
}
using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Abstractions.Identity;
using CogniVault.Platform.Identity.Provider;

using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Platform.Identity;
public class UserManagement : IUserManagement
{
    private readonly ICurrentUser _user;
    private readonly ITimeProvider _timeProvider;

    public UserManagement(IServiceProvider serviceProvider, ICurrentUser user)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _timeProvider = serviceProvider.GetService<ITimeProvider>() ?? new NullTimeProvider();
    }
    // implement IUserManagement methods
}

public interface IUserManagement
{
}
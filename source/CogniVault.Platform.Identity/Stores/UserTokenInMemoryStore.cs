using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Stores;

public class UserTokenInMemoryStore<TId> : IUserTokenStore<TId> where TId : struct
{
    // Using a ConcurrentDictionary to ensure thread safety.
    private readonly ConcurrentDictionary<TId, string> _tokenStore = new();

    public void StoreToken(TId userId, string token)
    {
        // If the user already has a token, it will be replaced.
        _tokenStore[userId] = token;
    }

    public string GetToken(TId userId)
    {
        _tokenStore.TryGetValue(userId, out var token);
        return token;
    }

    public void RemoveToken(TId userId)
    {
        _tokenStore.TryRemove(userId, out _);
    }
}

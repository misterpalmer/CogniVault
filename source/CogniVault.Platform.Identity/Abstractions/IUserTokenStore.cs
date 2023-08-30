using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Stores;

public interface IUserTokenStore<TId> where TId : struct
{
    void StoreToken(TId userId, string token);
    string GetToken(TId userId);
    void RemoveToken(TId userId);
}

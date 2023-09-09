// using CogniVault.Platform.Identity.Abstractions;
// using CogniVault.Platform.Identity.Attributes;
// using CogniVault.Platform.Identity.Constants;
// using CogniVault.Platform.Identity.Entities;
// using CogniVault.Platform.Identity.ValueObjects;

// namespace CogniVault.Platform.Identity.Repositories;

// [IdentityRepositoryProvider(IdentityRepositoryProviderKeyConstants.InMemory)]
// public class PlatformUserRepositoryInMemory : IPlatformUserRepository<PlatformUser>
// {
//     private readonly Dictionary<Guid, PlatformUser> _storage = new Dictionary<Guid, PlatformUser>();

//     public async Task<PlatformUser> GetByIdAsync(Guid id)
//     {
//         _storage.TryGetValue(id, out var user);
//         return await Task.FromResult(user);
//     }

//     public async Task<PlatformUser> GetByUsernameAsync(Username username)
//     {
//         var user = _storage.Values.FirstOrDefault(u => u.Username == username);
//         return await Task.FromResult(user);
//     }

//     public async Task<bool> IsValidUserCredentialsAsync(Username username, EncryptedPassword password)
//     {
//         var user = await GetByUsernameAsync(username);
//         if (user == null) return false;

//         return user.Password == password;  // You might want to replace this with a more secure password comparison.
//     }

//     public async Task AddAsync(PlatformUser platformUser)
//     {
//         if (!_storage.ContainsKey(platformUser.Id))
//         {
//             _storage[platformUser.Id] = platformUser;
//         }
//         // else, maybe throw an exception or handle duplicate additions based on your requirements.
        
//         await Task.CompletedTask;
//     }

//     public async Task UpdateAsync(PlatformUser platformUser)
//     {
//         // Update if exists, or add if it doesn't.
//         _storage[platformUser.Id] = platformUser;
//         await Task.CompletedTask;
//     }

//     public async Task DeleteAsync(Guid id)
//     {
//         _storage.Remove(id);
//         await Task.CompletedTask;
//     }
// }
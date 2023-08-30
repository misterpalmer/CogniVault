using System.Security.Claims;

using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Stores;
using CogniVault.Platform.Identity.ValueObjects;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CogniVault.Platform.Identity.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IPlatformUserRepository _userRepository;
    private readonly IUserTokenStore<Guid> _userTokenStore;
    private readonly IRolePermissionStore<IPlatformUser<Guid>, Guid> _rolePermissionStore;
    private readonly IJwtProvider _jwtProvider;

    public AuthorizationService(IPlatformUserRepository userRepository,
        IUserTokenStore<Guid> userTokenStore,
        IRolePermissionStore<IPlatformUser<Guid>, Guid> rolePermissionStore,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _userTokenStore = userTokenStore;
        _rolePermissionStore = rolePermissionStore;
        _jwtProvider = jwtProvider;
    }

    public async Task<IResult> AuthorizeUserAsync(HttpContext context,
        Username username,
        Password password)
    {
        // Check if the user credentials are valid
        if (await _userRepository.IsValidUserCredentialsAsync(username, password))
        {
            // Generate JWT
            var token = await _jwtProvider.GetJwtAsync(username.Value);

            // Store the token
            // _userTokenStore.StoreToken(await _userRepository.GetByUsernameAsync(username), token);

            // Authenticate user within the context
            await context.SignInAsync("cookie", new ClaimsPrincipal(new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.NameIdentifier, username.Value) },
                "cookie")));

            return Results.Ok(new { Token = token }); // Return token to the user
        }
        
        return Results.Unauthorized();
    }

    public IResult CheckUserAuthorization(IPlatformUser<Guid> user, PermissionName permission)
    {
        if (_rolePermissionStore.UserHasPermission(user.Id, permission))
        {
            return Results.Ok("User is authorized.");
        }

        var properties = new AuthenticationProperties
        {
            RedirectUri = "/path-to-login-or-error-page"  // specify the URI to redirect the user to.
        };

        return Results.Forbid(properties);
    }
}

using System.Security.Claims;

using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.Stores;
using CogniVault.Platform.Identity.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace CogniVault.Platform.Identity.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlatformUserService _userService;
    // private readonly IPlatformUserRepository<PlatformUser> _userRepository;
    private readonly IUserTokenStore<Guid> _userTokenStore;
    private readonly IRolePermissionStore<PlatformUser, Guid> _rolePermissionStore;
    private readonly IJwtProvider _jwtProvider;

    public AuthorizationService(IUnitOfWork unitOfWork,
        IPlatformUserService platformUserService,
        IUserTokenStore<Guid> userTokenStore,
        IRolePermissionStore<PlatformUser, Guid> rolePermissionStore,
        IJwtProvider jwtProvider)
    {
        _unitOfWork = unitOfWork;
        _userService = platformUserService;
        _userTokenStore = userTokenStore;
        _rolePermissionStore = rolePermissionStore;
        _jwtProvider = jwtProvider;
    }

    public async Task<IResult> AuthorizeUserAsync(HttpContext context, Username username, PlainPassword password)
    {
        // Check if the user credentials are valid
        if (await _userService.IsValidUserCredentialsAsync(username, password))
        {
            // Generate JWT
            var token = await _jwtProvider.GetJwtAsync(username.Value);

            // Fetch the user by username
            var user = await _userService.GetByUsernameAsync(username);

            // Store the token
            _userTokenStore.StoreToken(user.Id, token);

            return Results.Ok(new { Token = token }); // Return token to the user
        }

        return Results.Unauthorized();
    }

    public IResult CheckUserAuthorization(PlatformUser user, PermissionName permission)
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

using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IAuthorizationService
{
    Task<IResult> AuthorizeUserAsync(HttpContext context, Username username, PlainPassword password);
    IResult CheckUserAuthorization(PlatformUser user, PermissionName permission);
}

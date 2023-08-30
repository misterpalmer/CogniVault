using CogniVault.Platform.Identity.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IAuthorizationService
{
    Task<IResult> AuthorizeUserAsync(HttpContext context, Username username, Password password);
    IResult CheckUserAuthorization(IPlatformUser<Guid> user, PermissionName permission);
}

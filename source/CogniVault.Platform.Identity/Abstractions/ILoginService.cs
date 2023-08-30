using CogniVault.Platform.Identity.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Identity.Abstractions;

public interface ILoginService
{
    Task<IResult> HandleLoginAsync(HttpContext context, Username username);
}


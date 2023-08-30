using System.Security.Claims;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Identity.Services;

public class LoginService : ILoginService
{
    public async Task<IResult> HandleLoginAsync(HttpContext context, Username username)
    {
        await context.SignInAsync(
            "cookie",
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, username.Value)
                    },
                    "cookie"
                )
            ));

        return Results.Redirect("/someRedirectUrl"); // replace with your desired redirect URL
    }
}


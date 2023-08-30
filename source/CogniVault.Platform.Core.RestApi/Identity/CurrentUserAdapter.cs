using CogniVault.Platform.Core.Abstractions.Identity;

using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Core.RestApi.Identity;

public class CurrentUserAdapter : ICurrentUser
{
    protected readonly HttpContext _httpContext;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAdapter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
    }

    public virtual bool IsAuthenticated => _httpContext.User.Identity.IsAuthenticated;
    public virtual string UserName => _httpContext.User.Identity.Name;
}
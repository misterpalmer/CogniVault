using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Core.RestApi.Abstractions;

public interface IExceptionHandler
{
    bool CanHandle(Exception ex);
    Task HandleAsync(HttpContext context, Exception ex);
}
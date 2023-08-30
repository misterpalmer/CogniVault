using System.Net;
using System.Text.Json;

using CogniVault.Platform.Core.RestApi.Abstractions;
using CogniVault.Platform.Core.RestApi.Configuration;

using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Core.RestApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<IExceptionHandler> _exceptionHandlers;

    public ExceptionMiddleware(RequestDelegate next, IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        _next = next;
        _exceptionHandlers = exceptionHandlers ?? throw new ArgumentNullException(nameof(exceptionHandlers));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var handler = _exceptionHandlers.FirstOrDefault(h => h.CanHandle(ex));

            if (handler != null)
            {
                await handler.HandleAsync(httpContext, ex);
            }
        }
    }
}


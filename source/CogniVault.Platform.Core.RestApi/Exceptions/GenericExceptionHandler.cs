using System.Net;
using System.Net.Mime;
using System.Text.Json;
using CogniVault.Platform.Core.RestApi.Abstractions;
using CogniVault.Platform.Core.RestApi.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CogniVault.Platform.Core.RestApi.Exceptions;

public class GenericExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GenericExceptionHandler> _logger;

    public GenericExceptionHandler(ILogger<GenericExceptionHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public bool CanHandle(Exception ex) => true; // this handler can handle all exceptions

    public async Task HandleAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "An unexpected error occurred.");

        var response = context.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = MediaTypeNames.Application.Json;

        var errorResponse = new 
        {
            StatusCode = response.StatusCode,
            Message = "An unexpected error occurred."
        };

        await response.WriteAsync(JsonSerializer.Serialize(errorResponse, Common.DefaultJsonSerializerOptions));
    }
}
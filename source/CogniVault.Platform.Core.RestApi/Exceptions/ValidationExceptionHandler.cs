using System.Net;
using System.Net.Mime;
using System.Text.Json;
using CogniVault.Platform.Core.RestApi.Abstractions;
using CogniVault.Platform.Core.RestApi.Configuration;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CogniVault.Platform.Core.RestApi.Exceptions;

public class ValidationExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception ex) => ex is ValidationException;

    public async Task HandleAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        var validationException = ex as ValidationException;

        response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.ContentType = MediaTypeNames.Application.Json;

        var errors = validationException.Errors.Select(err => new 
        {
            PropertyName = err.PropertyName,
            ErrorMessage = err.ErrorMessage,
            ErrorCode = err.ErrorCode
        });

        var errorResponse = new 
        {
            StatusCode = response.StatusCode,
            Message = "Validation failed",
            Errors = errors
        };

        await response.WriteAsync(JsonSerializer.Serialize(errorResponse, Common.DefaultJsonSerializerOptions));
    }
}

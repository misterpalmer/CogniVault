using CogniVault.Platform.Core.Abstractions.Identity;
using CogniVault.Platform.Core.Collections;
using CogniVault.Platform.Core.RestApi.Responses;

using Microsoft.AspNetCore.Mvc;

namespace CogniVault.Platform.Core.RestApi.Controllers;

[ApiController]
public class AppApiController : ControllerBase
{
    protected readonly ICurrentUser _currentUser;

    public AppApiController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public IActionResult OkResponse<T>(T data, string message = null)
    {
        var outcome = OperationOutcome.SuccessfulOutcome;
        outcome.Message = message ?? string.Empty;

        var apiResponse = new ApiResponse<T>
        {
            Data = data,
            Outcome = outcome
        };

        return Ok(apiResponse);
    }

    public IActionResult OkPagedResponse<T>(IPagedList<T> data, string message = null)
    {
        var outcome = OperationOutcome.SuccessfulOutcome;
        outcome.Message = message ?? string.Empty;

        var apiResponse = new ApiResponse<IList<T>>
        {
            Data = data.Items,
            Outcome = outcome
        };

        Response.Headers.Add("X-Pagination", $"{nameof(data.IndexFrom)}:{data.IndexFrom}");
        return Ok(apiResponse);
    }

    public IActionResult OkResponse<T>(T data, OperationOutcome operationOutcome)
    {
        var apiResponse = new ApiResponse<T>
        {
            Data = data,
            Outcome = operationOutcome
        };

        return Ok(apiResponse);
    }

    public IActionResult OkResponse<T>(ApiResponse<T> apiResponse)
    {
        return Ok(apiResponse);
    }

    public IActionResult CreatedResponse<T>(T data, string url, string message = null)
    {
        var outcome = OperationOutcome.SuccessfulOutcome;
        outcome.Message = message ?? string.Empty;

        var apiResponse = new ApiResponse<T>
        {
            Data = data,
            Outcome = outcome
        };

        return Created(url, apiResponse);
    }

    public IActionResult CreatedResponse<T>(T data, string url, OperationOutcome operationOutcome)
    {
        var apiResponse = new ApiResponse<T>
        {
            Data = data,
            Outcome = operationOutcome
        };

        return Created(url, apiResponse);
    }

    public IActionResult CreatedResponse<T>(string url, ApiResponse<T> apiResponse)
    {
        return Created(url, apiResponse);
    }

}
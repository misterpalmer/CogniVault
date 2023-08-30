using CogniVault.Platform.Core.Abstractions.Identity;

using Microsoft.Extensions.Logging;

namespace CogniVault.Platform.Core.RestApi.Controllers;

public class AppApiLoggerController : AppApiController
{
    protected readonly ILogger<AppApiLoggerController> _logger;

    public AppApiLoggerController(ICurrentUser currentUser, ILogger<AppApiLoggerController> logger)
        : base(currentUser)
    {
        _logger = logger;
    }
}
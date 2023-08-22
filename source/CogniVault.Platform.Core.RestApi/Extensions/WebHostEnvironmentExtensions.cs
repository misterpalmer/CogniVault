using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CogniVault.Platform.Core.RestApi.Extensions;

public static class WebHostEnvironmentExtensions
{
    public static bool IsDevelopmentOrStaging(this IWebHostEnvironment webHostEnvironment)
    {
        return webHostEnvironment.IsDevelopment()
               || webHostEnvironment.IsStaging()
               || webHostEnvironment.EnvironmentName.ToUpper().Contains("localhost".ToUpper());
    }
}
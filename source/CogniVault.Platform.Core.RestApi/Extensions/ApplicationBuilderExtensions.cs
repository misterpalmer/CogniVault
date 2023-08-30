using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CogniVault.Platform.Core.RestApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureMiddlewareForEnvironments(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopmentOrStaging())
            app.UseDeveloperExceptionPage();
        else
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
    }
}
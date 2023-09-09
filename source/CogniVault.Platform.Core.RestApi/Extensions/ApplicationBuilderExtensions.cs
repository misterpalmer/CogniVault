using CogniVault.Platform.Core.RestApi.Constants;
using CogniVault.Platform.Core.Extensions;
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

    public static void UseSwaggerInDevAndStaging(this IApplicationBuilder app, IWebHostEnvironment hostingEnvironment, string[] versions)
    {
        if (hostingEnvironment.IsDevelopmentOrStaging())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var version in versions)
                {
                    c.DefaultModelsExpandDepth(-1);
                    c.SwaggerEndpoint($"{Swagger.EndPoint.Url.FormatAs($"{Swagger.Versions.VersionPrefix}{version}")}",
                        $"{Swagger.EndPoint.Name.FormatAs($"{Swagger.Versions.VersionPrefix}{version}")}");
                }
            });
        }
    }
}
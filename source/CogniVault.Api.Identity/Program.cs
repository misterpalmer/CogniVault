using Serilog;
using CogniVault.Platform.Core.Services;
using CogniVault.Platform.Identity.InMemoryProvider;
using CogniVault.Platform.Core.RestApi;
using CogniVault.Platform.Core.RestApi.Middleware;
using CogniVault.Platform.Identity.Services;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Api.Identity.Extensions;
using Microsoft.OpenApi.Models;
using CogniVault.Platform.Core.RestApi.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Authentication;
using CogniVault.Api.Identity.HostedServices;

namespace CogniVault.Api.Identity;
public class Program
{
    public static void Main(string[] args)
    {
        StaticLogger.EnsureInitialized();
        Log.Information("Starting CogniVault.Api.Identity");

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddLogging()
                .AddRestApi();
            builder.Services.AddJwt(builder.Configuration);

            // builder.WebHost.ConfigureKestrel(options =>
            // {
            //     options.ListenAnyIP(7166, listenOptions =>
            //     {
            //         listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            //         listenOptions.UseHttps("../../cognivault.pfx", "misterpalmer");
            //     });

            //     options.ConfigureHttpsDefaults(listenOptions =>
            //     {
            //         listenOptions.ClientCertificateMode = ClientCertificateMode.NoCertificate;
            //         listenOptions.SslProtocols = SslProtocols.Tls12;
            //     });
            // });

            // Add services to the container.
            builder.Services.AddInMemoryRepositories();
            builder.Services.AddSomeMoreRepositories();

            // Registering the required services for LoginController            
            builder.Services.AddHostedService<PlatformSeeder>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
                    });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();  // Add this middleware before UseAuthorization
            app.UseAuthorization();
            app.MapControllers();
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            //     // ... other endpoints
            // });

            app.Run();
        }
        catch (Exception ex) when (!ex.GetType().FullName!.Equals("HostAbortedException", StringComparison.Ordinal))
        {
            StaticLogger.EnsureInitialized();
            Log.Fatal("CogniVault.Api.Identity terminated unexpectedly");
            throw;
        }
        finally
        {
            StaticLogger.EnsureInitialized();
            Log.Information("Stopping CogniVault.Api.Identity");
            Log.CloseAndFlush();
        }
    }
}



// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options =>
//     {
//         options.Authority = "https://localhost:5001";
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateAudience = false
//         };
//     });

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("ApiScope", policy =>
//     {
//         policy.RequireAuthenticatedUser();
//         policy.RequireClaim("scope", "CogniVault.Api.Identity");
//     });
// });


// builder.Services.AddHealthChecks()
//     .AddCheck<HealthCheck>("CogniVault.Api.Identity");




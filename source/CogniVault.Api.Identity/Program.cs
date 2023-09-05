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
            builder.Services.AddLogging();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddRestApi();
            builder.Services.AddInMemoryRepositories();

            // Registering the required services for LoginController
            builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<LoginService>();
            builder.Services.AddTransient<IPasswordEncryptor, PasswordEncryptor>();

            // Learn more about configuring Swagger / OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API Swagger", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "CogniVault.Api.Identity.xml");
                swagger.IncludeXmlComments(filePath);
            });

            builder.Services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 7166;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.SeedPlatformUsers(builder.Configuration);

            builder.Services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "CogniVault.Api.Identity");
                });
            });

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




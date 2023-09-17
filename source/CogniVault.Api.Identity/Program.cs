using Serilog;
using CogniVault.Platform.Core.Services;
using CogniVault.Platform.Identity.InMemoryProvider;
using CogniVault.Platform.Core.RestApi;
using CogniVault.Platform.Core.RestApi.Middleware;
using CogniVault.Api.Identity.Extensions;
using CogniVault.Api.Identity.HostedServices;
using CogniVault.Platform.Identity.EFCoreProvider;
using Microsoft.EntityFrameworkCore.Design;
using CogniVault.Platform.Identity;




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


            // Add services to the container.
            builder.Services
                .AddLogging()
                .AddRestApi();
            builder.Services.AddJwt(builder.Configuration);
            builder.Services.AddIdentityValidators();
            builder.Services.AddInMemoryTokenStores();
            builder.Services.AddAuthorizationServices();
            builder.Services.AddIdentityEFCoreProvider(builder.Configuration);
            builder.Services.AddEFCoreRepositories();
            builder.Services.AddHostedService<PlatformSeederEFCore>();
            // builder.Services.AddInMemoryRepositories();
            // builder.Services.AddSomeMoreRepositories();
            builder.Services.AddHostedService<PlatformSeeder>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            app.Services.MigrateIdentityEFCoreProvider<IdentityContext>();

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




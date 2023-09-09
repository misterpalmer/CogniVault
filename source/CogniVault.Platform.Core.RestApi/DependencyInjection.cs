using CogniVault.Platform.Core.RestApi.Abstractions;
using CogniVault.Platform.Core.RestApi.Configuration;
using CogniVault.Platform.Core.RestApi.Exceptions;

using Microsoft.OpenApi.Models;


namespace CogniVault.Platform.Core.RestApi;

public static class DependencyInjection
{
    public static IServiceCollection AddRestApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services
            .AddControllers()
            .AddJsonOptions(JsonSerializerOptionsConfigurer.ConfigureDefaultJsonOptions);

        services
            .AddTransient<IExceptionHandler, ValidationExceptionHandler>()
            .AddTransient<IExceptionHandler, GenericExceptionHandler>();

        // Learn more about configuring Swagger / OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API Swagger", Version = "v1" });
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "CogniVault.Api.Identity.xml");
            swagger.IncludeXmlComments(filePath);
        });

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            options.HttpsPort = 7166;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });


        return services;
    }
}
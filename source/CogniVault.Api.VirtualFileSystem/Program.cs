using Serilog;
using CogniVault.Platform.Core.Services;
using CogniVault.Api.VirtualFileSystem.HostedServices;
using CogniVault.Api.VirtualFileSystem.Extensions;
using Microsoft.OpenApi.Models;
using CogniVault.Platform.Core.RestApi.Middleware;
using CogniVault.Platform.Core.RestApi.Configuration;
using System.Security.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using CogniVault.Application.VirtualFileSystem.Provider;

StaticLogger.EnsureInitialized();
Log.Information("Starting CogniVault.Api.VirtualFileSystem");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddLogging();

    // builder.WebHost.ConfigureKestrel(options =>
    // {
    //     options.ListenAnyIP(7167, listenOptions =>
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
    builder.Services
        .AddControllers()
        .AddJsonOptions(JsonSerializerOptionsConfigurer.ConfigureDefaultJsonOptions);
    // builder.Services.AddRestApi();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(swagger =>
    {
        swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "File Manager API Swagger", Version = "v1" });
        var filePath = Path.Combine(System.AppContext.BaseDirectory, "CogniVault.Api.VirtualFileSystem.xml");
        swagger.IncludeXmlComments(filePath);
    });

    builder.Services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
        options.LowercaseQueryStrings = true;
    });

    builder.Services.AddHttpClient<FileSystemSeeder>(c =>
    {
        // Configure the HttpClient instance here.
        c.DefaultRequestHeaders.Add("User-Agent", "CogniVault");
        // ... other configurations
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            // Configure the HttpClientHandler instance here
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            // ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12
        };
        
        return handler;
    });

    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
        options.HttpsPort = 7167;
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

    builder.Services.AddVirtualFileSystemMemoryProvider();
    builder.Services.SeedFileSystem(builder.Configuration);

    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileManager API v1");
            });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().FullName!.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal("CogniVault.Api.VirtualFileSystem terminated unexpectedly");
    throw;
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Stopping CogniVault.Api.VirtualFileSystem");
    Log.CloseAndFlush();
}



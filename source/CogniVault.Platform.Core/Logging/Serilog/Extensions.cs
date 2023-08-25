using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace CogniVault.Platform.Core.Logging.Serilog;

public static class Extensions
{
    public static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<LoggerSettings>().BindConfiguration(nameof(LoggerSettings));

        _ = builder.Host.UseSerilog((_, context, loggerConfiguration) =>
        {
            var settings = context.GetRequiredService<IOptions<LoggerSettings>>().Value;
            string applicationName = settings.ApplicationName;
            LogEventLevel minimumLogLevel = Enum.Parse<LogEventLevel>(settings.MinimumLogLevel);

        });
    }

    public static void ConfigureEnrichers(LoggerConfiguration serilogConfiguration, string applicationName)
    {
        serilogConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", applicationName)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.FromLogContext();
    }

    public static void ConfigureMinimumLevel(LoggerConfiguration serilogConfiguration, LogEventLevel minimumLogLevel)
    {
        serilogConfiguration.MinimumLevel.Is(minimumLogLevel);
    }

    public static void ConfigureWriteToConsole(LoggerConfiguration serilogConfiguration, bool writeToConsole)
    {
        if (writeToConsole)
        {
            serilogConfiguration.WriteTo.Console(
                formatter: new CompactJsonFormatter(),
                restrictedToMinimumLevel: LogEventLevel.Information);
        }
    }

    public static void ConfigureWriteToFile(LoggerConfiguration serilogConfiguration, bool writeToFile)
    {
        if (writeToFile)
        {
            serilogConfiguration.WriteTo.File(
                formatter: new CompactJsonFormatter(),
                path: "Logs\\log-.json",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                restrictedToMinimumLevel: LogEventLevel.Information);
        }
    }

    public static void ConfigureWriteToSeq()
    {

    }
}
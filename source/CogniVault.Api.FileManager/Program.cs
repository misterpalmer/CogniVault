using Serilog;
using CogniVault.Platform.Core.Services;

StaticLogger.EnsureInitialized();
Log.Information("Starting CogniVault.Api.FileManager");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().FullName!.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal("CogniVault.Api.FileManager terminated unexpectedly");
    throw;
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Stopping CogniVault.Api.FileManager");
    Log.CloseAndFlush();
}



using NLog;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

        var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapSwagger();
        app.UseSwaggerUI();
        app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex, "Seopped program because of exception");
}
finally
{
    LogManager.Shutdown();
}
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Extensions;
using Demo.Worker;
using Serilog;
using System.Reflection;

var configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json",false)
                       .AddEnvironmentVariables()
                       .Build();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

try
{
    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                         throw new InvalidOperationException("ConnectionString 'DefaultConnection' not found");
    var migrationAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    //var builder = Host.CreateApplicationBuilder(args);
    //builder.Services.AddHostedService<Worker>();

    //var host = builder.Build();
    //host.Run();
    IHost host = Host.CreateDefaultBuilder(args)
                     .UseWindowsService()
                     .UseSerilog()
                     .ConfigureServices(services =>
                     {
                         services.AddHostedService<Worker>();
                         services.AddApplicationDbContext(connectionString, migrationAssembly);
                     })
                     .Build();
    await host.RunAsync();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application Crashed");
}
finally
{
    Log.CloseAndFlush();
}

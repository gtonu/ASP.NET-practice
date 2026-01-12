using Demo.Infrastructure.Data;
using Demo.Infrastructure.Extensions;
using Demo.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

#region BootstrapLogger configuration
Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/web-log-.log", rollingInterval: RollingInterval.Day)
            .CreateBootstrapLogger();
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var migrationAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext));

    #region General Logger configuration
    builder.Host.UseSerilog((context, lc) => lc
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft",Serilog.Events.LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .ReadFrom.Configuration(context.Configuration)
    );
    #endregion

    #region configuring ApplicationDbContext
    builder.Services.AddApplicationDbContext(connectionString, migrationAssembly);
    #endregion
    //builder.Services.AddKeyedTransient<IEmailUtility, EmailUtility>("Service1");
    //builder.Services.AddKeyedTransient<IEmailUtility, HtmlUtility>("Service2");
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddRazorPages();

    #region configuring ModifiedIdentiy options
    builder.Services.AddModifiedIdentity();
    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    app.MapRazorPages()
       .WithStaticAssets();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application Crashed");
}
finally
{
    Log.CloseAndFlush();
}


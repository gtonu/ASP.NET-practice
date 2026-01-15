using Demo.Infrastructure.Data;
using Demo.Infrastructure.Extensions;
using Demo.Web.Models;
using Mapster;
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
    var googleClientId = builder.Configuration["web:client_id"];
    var googleClientSecret = builder.Configuration["web:client_secret"];

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

    #region Google login configuration
    builder.Services.AddAuthentication().AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = googleClientId;
        googleOptions.ClientSecret = googleClientSecret;
    });
    #endregion

    #region configuring ApplicationDbContext
    builder.Services.AddApplicationDbContext(connectionString, migrationAssembly);
    #endregion

    #region Mapster configuration
    builder.Services.AddMapster();
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

    //Admin area routing configuration..
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

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


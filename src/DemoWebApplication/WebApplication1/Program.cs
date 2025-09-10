using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication1.Data;
using WebApplication1.Domain.Interfaces;
using WebApplication1.Models;

//Configuring Bootstrap Logger
#region bootstrap_logger
Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log_book-.log",rollingInterval:RollingInterval.Day)
                .CreateBootstrapLogger();
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    #region Serilog Configuration
    builder.Host.UseSerilog((context, lc) => lc
       .MinimumLevel.Debug()
       .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
       .Enrich.FromLogContext()
       .ReadFrom.Configuration(builder.Configuration)
        );
    #endregion

    //throw new Exception("Application Crashed");

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    builder.Services.AddTransient<IEmailUtility, EmailUtility>();
    builder.Services.AddSingleton<IEmailUtility, HtmlEmailUtility>();
    builder.Services.AddScoped<IEmailUtility, HtmlEmailUtility>();

    
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

    Log.Information("Application Strated");

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex,"Application Crashed");
}
finally
{
    Log.CloseAndFlush();
}

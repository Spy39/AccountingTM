using Accounting.Data;
using AccountingTM.Authorization;
using AccountingTM.Domain;
using AccountingTM.Domain.Authorization;
using AccountingTM.Domain.Seeds;
using AccountingTM.Localization;
using AccountingTM.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Запуск приложения!");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews(x =>
{
    x.Filters.Add(typeof(ExceptionFilter));
}).AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var connectionString = builder.Configuration.GetConnectionString("Default");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(connectionString));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Account/Login");
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserManager, CurrentUserManager>();
builder.Services.AddSingleton(x => new LocalizationManager("Localization/Resources"));
builder.Services.AddScoped<PermissionChecker>();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TechnicalEquipment}/{action=Index}/{id?}");
PermissionProvider.SetPermissions();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
    new RoleSeed(dbContext).Seed();
}

app.Run();

using Accounting.Data;
using AccountingTM.Authorization;
using AccountingTM.Domain;
using AccountingTM.Domain.Authorization;
using AccountingTM.Localization;
using AccountingTM.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

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

app.Run();

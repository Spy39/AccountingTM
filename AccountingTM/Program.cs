using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AccountingTM.Domain;
using Accounting.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using AccountingTM.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews(x =>
{
    x.Filters.Add(typeof(ExceptionFilter));
}).AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(connectionString));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/Account/Login");

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TechnicalEquipment}/{action=Index}/{id?}");

app.Run();

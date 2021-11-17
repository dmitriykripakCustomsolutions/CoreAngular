using DataAccess.DataContexts.ApplicationDbContext;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Environment.EnvironmentName
var connStr = builder.Configuration.GetSection($"DBSettings:{builder.Environment.EnvironmentName}:SqlServerConnectionString")?.Value ?? "";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddControllersWithViews().AddOData(options => options.Select().Filter().OrderBy());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

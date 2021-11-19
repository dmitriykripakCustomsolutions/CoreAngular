using Audit.WebApi;
using Audit.EntityFramework;
using DataAccess.DataContexts.ApplicationDbContext;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Environment.EnvironmentName
var connStr = builder.Configuration.GetSection($"DBSettings:{builder.Environment.EnvironmentName}:SqlServerConnectionString")?.Value ?? "";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddControllersWithViews(mvc => {
    mvc.AddAuditFilter(config => config
        .LogActionIf(d => true)
        .WithEventType("{verb}.{controller}.{action}")
        .IncludeHeaders(ctx => !ctx.ModelState.IsValid)
        .IncludeRequestBody()
        .IncludeModelState()
        .IncludeResponseBody(ctx => ctx.HttpContext.Response.StatusCode == 200));
}).AddOData(options => options.Select().Filter().OrderBy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();

app.UseSwagger(x=> x.SerializeAsV2=true);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


var logConn = builder.Configuration.GetSection($"LogSettings:{builder.Environment.EnvironmentName}:ConnectionString")?.Value ?? "";
var logConnDatabase = builder.Configuration.GetSection($"LogSettings:{builder.Environment.EnvironmentName}:Database")?.Value ?? "";
var logConnCollection = builder.Configuration.GetSection($"LogSettings:{builder.Environment.EnvironmentName}:Collection")?.Value ?? "";

Audit.Core.Configuration.DataProvider = new Audit.MongoDB.Providers.MongoDataProvider()
{
    ConnectionString = logConn,
    Database = logConnDatabase,
    Collection = logConnCollection
};

app.MapFallbackToFile("index.html"); ;

app.Run();

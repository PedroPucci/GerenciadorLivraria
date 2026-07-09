using GerenciadorLivraria.Extensions.Configuration;
using GerenciadorLivraria.Extensions.ExtensionsLogs;
using GerenciadorLivraria.Extensions.Middlewares;
using GerenciadorLivraria.Extensions.SwaggerDocumentation;
using GerenciadorLivraria.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSecurityServices();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "GerenciadorLivraria:";
});
builder.Services.AddHealthChecks().AddDbContextCheck<DataContext>("sqlserver");

LogExtension.InitializeLogger();
var loggerSerialLog = LogExtension.GetLogger();
loggerSerialLog.Information("Logging initialized.");

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentation();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseSecurityPipeline();
app.MapControllers();
app.UseCustomHealthChecks();
app.ApplyMigrations();
app.Run();
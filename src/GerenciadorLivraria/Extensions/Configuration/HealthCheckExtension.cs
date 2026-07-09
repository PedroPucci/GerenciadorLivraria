using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

namespace GerenciadorLivraria.Extensions.Configuration;

public static class HealthCheckExtension
{
    public static WebApplication UseCustomHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var result = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(x => new
                    {
                        name = x.Key,
                        status = x.Value.Status.ToString()
                    })
                };

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(result));
            }
        });

        return app;
    }
}
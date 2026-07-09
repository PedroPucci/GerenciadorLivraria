using GerenciadorLivraria.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Extensions.Configuration;

public static class DatabaseExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var runMigrations = app.Configuration.GetValue<bool>("RunMigrations");

        if (!runMigrations)
            return;

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration!");
        }
    }
}
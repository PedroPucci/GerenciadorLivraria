namespace GerenciadorLivraria.Extensions.Configuration;

public static class SecurityExtension
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddSession();
        services.AddHttpContextAccessor();
        return services;
    }

    public static WebApplication UseSecurityPipeline(this WebApplication app)
    {
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
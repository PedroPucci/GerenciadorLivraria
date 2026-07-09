namespace GerenciadorLivraria.Extensions.SwaggerDocumentation;

public static class SwaggerExtensions
{
    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
        });

        return app;
    }
}
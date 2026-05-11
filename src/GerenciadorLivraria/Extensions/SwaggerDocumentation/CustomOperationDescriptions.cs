using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GerenciadorLivraria.Extensions.SwaggerDocumentation
{
    public class CustomOperationDescriptions : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.ApiDescription?.HttpMethod is null || context.ApiDescription.RelativePath is null)
                return;

            var path = context.ApiDescription.RelativePath.ToLowerInvariant();

            var routeHandlers = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
            };

            foreach (var kv in routeHandlers
                     .OrderByDescending(k => k.Key.Length))
            {
                if (path.Contains(kv.Key))
                {
                    kv.Value.Invoke();
                    return;
                }
            }
        }

        private void HandleUsersOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            
        }

        private void HandleGamesOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            
        }

        private void AddResponses(OpenApiOperation operation, string statusCode, string description)
        {
            if (!operation.Responses.ContainsKey(statusCode))
            {
                operation.Responses.Add(statusCode, new OpenApiResponse { Description = description });
            }
        }
    }
}
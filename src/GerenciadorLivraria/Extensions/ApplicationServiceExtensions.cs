using GerenciadorLivraria.Application.Abstractions.Persistence;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Extensions.SwaggerDocumentation;
using GerenciadorLivraria.Infrastructure.Connections;
using GerenciadorLivraria.Infrastructure.Repository;
using GerenciadorLivraria.Infrastructure.Repository.Interfaces;
using GerenciadorLivraria.Infrastructure.Repository.RepositoryUoW;
using GerenciadorLivraria.Shared.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace GerenciadorLivraria.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();

            var jwtSettings = config.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var secretKey = jwtSettings["SecretKey"];

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("WebApiDatabase"));
            });

            services.AddIdentity<UserEntity, ProfileEntity>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey!)),

                    ClockSkew = TimeSpan.FromMinutes(5),
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(
                            "{\"success\": false, \"message\": \"Invalid or expired token. Please log in again.\"}");
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(
                            "{\"success\": false, \"message\": \"You do not have permission to access this resource.\"}");
                    }
                };
            });

            services.AddAuthorization();

            services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.CustomSchemaIds(t => t.FullName);
                opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API GerenciadorLivraria",
                    Description = @"
                        A API GerenciadorLivraria é uma solução moderna para gerenciamento de livros de uma livraria.
                        Desenvolvida em .NET 8, a aplicação permite realizar operações de cadastro, consulta, atualização e remoção de livros, 
                        seguindo boas práticas de arquitetura e desenvolvimento.

                        Principais Recursos:
                        - CRUD completo de livros.
                        - Validações utilizando FluentValidation.
                        - Documentação automatizada com Swagger.
                        - Logs estruturados com Serilog.
                        - Testes unitários e BDD.
                        - Arquitetura organizada em camadas.
                        - Health Checks para monitoramento da aplicação.

                        A solução foi construída com foco em escalabilidade, organização, manutenibilidade e boas práticas de desenvolvimento backend.
                    "
                });

                opt.OperationFilter<CustomOperationDescriptions>();

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Cole apenas o token JWT, sem escrever Bearer."
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                if (File.Exists(xmlPath))
                {
                    opt.IncludeXmlComments(xmlPath);
                }
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:4200");
                });
            });

            services.AddScoped<IRepositoryUoW, RepositoryUoW>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuthenticationService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
    }
}
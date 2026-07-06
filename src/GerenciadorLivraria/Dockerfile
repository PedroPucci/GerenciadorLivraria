FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/GerenciadorLivraria/GerenciadorLivraria.csproj", "src/GerenciadorLivraria/"]
COPY ["src/GerenciadorLivraria.Application/GerenciadorLivraria.Application.csproj", "src/GerenciadorLivraria.Application/"]
COPY ["src/GerenciadorLivraria.Domain/GerenciadorLivraria.Domain.csproj", "src/GerenciadorLivraria.Domain/"]
COPY ["src/GerenciadorLivraria.Infrastructure/GerenciadorLivraria.Infrastructure.csproj", "src/GerenciadorLivraria.Infrastructure/"]
COPY ["src/GerenciadorLivraria.Shared/GerenciadorLivraria.Shared.csproj", "src/GerenciadorLivraria.Shared/"]

RUN dotnet restore "src/GerenciadorLivraria/GerenciadorLivraria.csproj"

COPY . .

WORKDIR "/src/src/GerenciadorLivraria"
RUN dotnet publish "GerenciadorLivraria.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GerenciadorLivraria.dll"]
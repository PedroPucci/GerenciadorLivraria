FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore ./src/GerenciadorLivraria/GerenciadorLivraria.csproj
RUN dotnet publish ./src/GerenciadorLivraria/GerenciadorLivraria.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "GerenciadorLivraria.dll"]
FROM mcr.microsoft.com/dotnet/sdk:5.0.201-buster-slim AS build
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ["src/GithubIntegration.Host/GithubIntegration.Host.csproj", "src/GithubIntegration.Host/"]
COPY ["src/GithubIntegration.Contracts/GithubIntegration.Contracts.csproj", "src/GithubIntegration.Contracts/"]
COPY ["src/GithubIntegration.Domain/GithubIntegration.Domain.csproj", "src/GithubIntegration.Domain/"]

RUN dotnet restore "src/GithubIntegration.Host/GithubIntegration.Host.csproj"

# Publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0.4-buster-slim

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GithubIntegration.Host.dll"]
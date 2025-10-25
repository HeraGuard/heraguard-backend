# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["heraguard.API/heraguard.API.csproj", "heraguard.API/"]
COPY ["heraguard.Application/heraguard.Application.csproj", "heraguard.Application/"]
COPY ["heraguard.Domain/heraguard.Domain.csproj", "heraguard.Domain/"]
COPY ["heraguard.Infrastructure/heraguard.Infrastructure.csproj", "heraguard.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "heraguard.API/heraguard.API.csproj"

# Copiar todo el código
COPY . .

# Build
WORKDIR "/src/heraguard.API"
RUN dotnet build "heraguard.API.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "heraguard.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer puerto
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "heraguard.API.dll"]

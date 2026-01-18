# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY src/TravelMgt.Api/TravelMgt.Api.csproj src/TravelMgt.Api/
COPY src/TravelMgt.Application/TravelMgt.Application.csproj src/TravelMgt.Application/
COPY src/TravelMgt.Domain/TravelMgt.Domain.csproj src/TravelMgt.Domain/
COPY src/TravelMgt.Infrastructure/TravelMgt.Infrastructure.csproj src/TravelMgt.Infrastructure/

# Restore
RUN dotnet restore src/TravelMgt.Api/TravelMgt.Api.csproj

# Copy source and publish
COPY . .
RUN dotnet publish src/TravelMgt.Api/TravelMgt.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TravelMgt.Api.dll"]

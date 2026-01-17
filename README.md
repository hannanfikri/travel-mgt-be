# Travel Booking Management API

A .NET 9 Web API backend using Clean Architecture with Domain, Application, Infrastructure, and API layers.

## Project layout

- `src/TravelMgt.Domain`: Core entities and interfaces.
- `src/TravelMgt.Application`: Use cases, DTOs, validators.
- `src/TravelMgt.Infrastructure`: EF Core, repositories, auth, PostgreSQL.
- `src/TravelMgt.Api`: REST API, auth, Swagger, CORS, Serilog.
- `tests/`: Unit and integration tests.

## Configuration

Update JWT and database settings in `src/TravelMgt.Api/appsettings.json` or via environment variables:

- `ConnectionStrings__Default`
- `DatabaseProvider` (set to `InMemory` for tests)
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__SigningKey`
- `Jwt__ExpiryMinutes`

Default seeded admin user (created on first run):

- Email: `admin@travelmgt.local`
- Password: `P@ssw0rd!`

## Database migrations

Run EF Core migrations from the repo root:

```bash
DOTNET_ENVIRONMENT=Development dotnet ef migrations add InitialCreate --project src/TravelMgt.Infrastructure --startup-project src/TravelMgt.Api
DOTNET_ENVIRONMENT=Development dotnet ef database update --project src/TravelMgt.Infrastructure --startup-project src/TravelMgt.Api
```

## Run locally

```bash
dotnet run --project src/TravelMgt.Api
```

Swagger UI is available in development at `/swagger`.

## Run with Docker

```bash
docker compose up --build
```

## Tests

```bash
dotnet test
```

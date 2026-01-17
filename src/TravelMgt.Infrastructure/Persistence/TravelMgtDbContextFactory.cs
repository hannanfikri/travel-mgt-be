using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TravelMgt.Infrastructure.Persistence;

public sealed class TravelMgtDbContextFactory : IDesignTimeDbContextFactory<TravelMgtDbContext>
{
    public TravelMgtDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("Default")
            ?? "Host=localhost;Port=5432;Database=travelmgt;Username=travelmgt_user;Password=user123";

        var optionsBuilder = new DbContextOptionsBuilder<TravelMgtDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TravelMgtDbContext(optionsBuilder.Options);
    }
}

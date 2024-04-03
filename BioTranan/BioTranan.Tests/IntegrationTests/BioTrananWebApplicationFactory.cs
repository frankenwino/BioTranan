using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BioTranan.Core.Database;

namespace BioTranan.Tests.IntegrationTests.BioTranan.Api;

internal class BioTrananWebApplicationFactory : WebApplicationFactory<Program>
{
    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove the existing DbContextOptions
            services.RemoveAll(typeof(DbContextOptions<BioTrananDbContext>));

            // Register a new DBContext that will use our test connection string
            string? connectionString = "Data Source=test.db";
            services.AddSqlite<BioTrananDbContext>(connectionString);

            // Delete the database (if exists) to ensure we start clean
            BioTrananDbContext dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        });
    }

    private static string? GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<BioTrananWebApplicationFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("BioTranan");
        return connectionString;
    }

    private static BioTrananDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BioTrananDbContext>();
        return dbContext;
    }
}
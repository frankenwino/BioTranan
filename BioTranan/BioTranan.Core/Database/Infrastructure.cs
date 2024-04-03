using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BioTranan.Core.Database;

public static class Infrastructure
{
    public static void AddDbContext(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<BioTrananDbContext>(opt => opt.UseSqlite("Data Source=../database.db"));
    }
}
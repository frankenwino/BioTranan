using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BioTranan.Core.Database;

public class DbContextFactory : IDesignTimeDbContextFactory<BioTrananDbContext>
{
    public BioTrananDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BioTrananDbContext>();
        optionsBuilder.UseSqlite("Data Source=../database.db");
        return new BioTrananDbContext(optionsBuilder.Options);
    }
}
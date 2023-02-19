using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
{
    public ECommerceAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Confıguration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}
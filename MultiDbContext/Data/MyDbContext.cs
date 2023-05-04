using Microsoft.EntityFrameworkCore;

namespace MultiDbContext.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions) : base(
        dbContextOptions)
    {
    }

    public DbSet<Product> Products { get; set; }
}
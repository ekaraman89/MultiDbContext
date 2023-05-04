using Microsoft.EntityFrameworkCore;

namespace MultiDbContext.Data;

public class MyDbContext : DbContext
{
    private readonly string _readConnectionString;
    private readonly string _writeConnectionString;

    public MyDbContext(DbContextOptions<MyDbContext> dbContextOptions, IConfiguration configuration) : base(
        dbContextOptions)
    {
        _readConnectionString = configuration.GetConnectionString("ReadDb");
        _writeConnectionString = configuration.GetConnectionString("WriteDb");
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(IsWritingOperation ? _writeConnectionString : _readConnectionString);
    }

    public bool IsWritingOperation { get; set; }
}
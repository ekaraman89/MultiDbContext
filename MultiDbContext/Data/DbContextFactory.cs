using Microsoft.EntityFrameworkCore;

namespace MultiDbContext.Data;

public class DbContextFactory : IDbContextFactory
{
    private readonly string _readConnectionString;
    private readonly string _writeConnectionString;

    public DbContextFactory(IConfiguration configuration)
    {
        _readConnectionString = configuration.GetConnectionString("ReadDb");
        _writeConnectionString = configuration.GetConnectionString("WriteDb");
    }

    public MyDbContext CreateDbContext(bool isWritingOperation = false)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

        optionsBuilder.UseNpgsql(isWritingOperation ? _writeConnectionString : _readConnectionString);
        return new MyDbContext(optionsBuilder.Options);
    }
}
namespace MultiDbContext.Data;

public interface IDbContextFactory
{
    MyDbContext CreateDbContext(bool isWritingOperation = false);
}
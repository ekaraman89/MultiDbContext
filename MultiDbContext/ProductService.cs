using MultiDbContext.Data;

namespace MultiDbContext;

public class ProductService : IProductService
{
    private readonly IDbContextFactory _contextFactory;

    public ProductService(IDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void AddProduct(string name)
    {
        Random rnd = new();
        Product product = new() { Name = name, Price = rnd.Next(1, 10) };

        var _dbContext = _contextFactory.CreateDbContext(true);

        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public List<Product> GetAll()
    {
        var _dbContext = _contextFactory.CreateDbContext();

        List<Product> products = _dbContext.Products.ToList();
        return products;
    }
}
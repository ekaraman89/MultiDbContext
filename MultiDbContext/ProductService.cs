using MultiDbContext.Data;

namespace MultiDbContext;

public class ProductService : IProductService
{
    private readonly MyDbContext _dbContext;

    public ProductService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddProduct(string name)
    {
        Random rnd = new();
        Product product = new() { Name = name, Price = rnd.Next(1, 10) };
        _dbContext.IsWritingOperation = true;
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }
 
    public List<Product> GetAll()
    {
        _dbContext.IsWritingOperation = false;
        List<Product> products = _dbContext.Products.ToList();
        return products;
    }
}
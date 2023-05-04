using MultiDbContext.Data;

namespace MultiDbContext
{
    public interface IProductService
    {
        void AddProduct(string name);
        List<Product> GetAll();
    }
}
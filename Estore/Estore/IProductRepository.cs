namespace Estore;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task SeedInitialDataAsync();
}
using Microsoft.EntityFrameworkCore;

namespace Estore; 
public class ProductRepository : IProductRepository
{
    private readonly EStoreContext _context;

    public ProductRepository(EStoreContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task SeedInitialDataAsync()
    {
        // Verificăm dacă baza de date este goală
        if (!await _context.Products.AnyAsync())
        {
            var defaultProducts = new List<Product>
            {
                new Product("Laptop Gaming ASUS ROG", 6500),
                new Product("MacBook Air M2", 5500),
                new Product("Monitor LED 27 inch", 1200),
                new Product("Tastatura Mecanica RGB", 450),
                new Product("Mouse Wireless Logitech", 300),
                new Product("Casti HyperX Cloud II", 350),
                new Product("Scaun Gaming Ergonomic", 950),
                new Product("Imprimanta Laser HP", 800),
                new Product("Webcam Full HD", 250),
                new Product("SSD extern 1TB", 500)
            };

            await _context.Products.AddRangeAsync(defaultProducts);
            await _context.SaveChangesAsync();
            Console.WriteLine("[SEED]: Catalogul a fost populat cu 10 produse noi.");
        }
    }
}
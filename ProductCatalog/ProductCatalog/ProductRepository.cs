using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();
        void Add(Product p);
    }

    public class ProductRepository : IProductRepository
    {
        private List<Product> _db = new List<Product> {
            new Product { Id = 1, Name = "Laptop Dell", Category = "Tech", Price = 4500 },
            new Product { Id = 2, Name = "Mouse MX Master", Category = "Tech", Price = 350 },
            new Product { Id = 3, Name = "Scaun Ergonomic", Category = "Home", Price = 1200 },
            new Product { Id = 4, Name = "Monitor 4K Sony", Category = "Tech", Price = 2800 },
            new Product { Id = 5, Name = "Canapea Extensibila", Category = "Home", Price = 2200 },
            new Product { Id = 6, Name = "Tastatura Mecanica", Category = "Tech", Price = 600 }
        };

        public IQueryable<Product> GetProducts() => _db.AsQueryable();
        public void Add(Product p) => _db.Add(p);
    }
}
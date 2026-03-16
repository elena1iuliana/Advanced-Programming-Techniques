using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog
{
    public class Cart
    {
        public List<Product> Content { get; } = new List<Product>();
        public void Add(Product p) => Content.Add(p);
        public decimal GetTotal() => Content.Sum(p => p.Price);
    }
}
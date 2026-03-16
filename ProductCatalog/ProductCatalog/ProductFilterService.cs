using System.Linq;

namespace ProductCatalog
{
    public class ProductFilterService
    {
        public IQueryable<Product> ApplyCriteria(IQueryable<Product> query, string cat, decimal? max, string sortOrder)
        {
            
            if (!string.IsNullOrEmpty(cat))
                query = query.Where(p => p.Category.ToLower() == cat.ToLower());

            if (max.HasValue)
                query = query.Where(p => p.Price <= max.Value);

          
            return sortOrder switch
            {
                "1" => query.OrderBy(p => p.Price),
                "2" => query.OrderByDescending(p => p.Price),
                "3" => query.OrderBy(p => p.Name),         
                "4" => query.OrderByDescending(p => p.Name), 
                _ => query.OrderBy(p => p.Price)
            };
        }
    }
}
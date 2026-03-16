using System;
using System.Linq;

namespace ProductCatalog
{
    public class ProductPresentationService
    {
        public void DisplayGrouped(IQueryable<Product> query)
        {
            
            var groups = from p in query
                         group p by p.Category into g
                         select new { CategoryName = g.Key, Items = g.ToList() };

            if (!groups.Any())
            {
                Console.WriteLine("\n[!] Nu exista produse care sa corespunda filtrelor tale.");
                return;
            }

            foreach (var g in groups)
            {
                Console.WriteLine($"\n--- [{g.CategoryName.ToUpper()}] ---");
                foreach (var item in g.Items)
                    Console.WriteLine($"  ID: {item.Id} | {item.Name.PadRight(20)} | Pret: {item.Price} RON");
            }
        }
    }
}
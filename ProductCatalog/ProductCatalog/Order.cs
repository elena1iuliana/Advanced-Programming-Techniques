using System.Collections.Generic;

namespace ProductCatalog
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
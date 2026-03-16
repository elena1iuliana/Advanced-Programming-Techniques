using System;
using System.Collections.Generic;

namespace EStore
{
    public class ShoppingCart
    {
        // Lista de produse din coș
        private List<Produs> items = new List<Produs>();

        // Metodă pentru adăugarea produselor în coș
        public void AddProduct(Produs p)
        {
            if (p != null)
            {
                items.Add(p);
                Console.WriteLine($"[Coș] Produs adăugat: {p.Name} ({p.Price} RON)");
            }
        }

        // Această metodă este esențială pentru clasa Order!
        public List<Produs> GetItems()
        {
            return items;
        }

        // Calculează suma totală a produselor din coș
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in items)
            {
                total += item.Price;
            }
            return total;
        }

        public void Clear()
        {
            items.Clear();
            Console.WriteLine("[Coș] Coșul a fost golit.");
        }
    }
}
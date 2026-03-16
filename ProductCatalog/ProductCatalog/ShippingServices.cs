using System;

namespace ProductCatalog
{
    public interface IShippingProvider
    {
        void Deliver(Order order);
    }

    public class ShippingService : IShippingProvider
    {
        public void Deliver(Order order)
        {
            Console.WriteLine("\n--- OPȚIUNI LIVRARE ---");
            Console.WriteLine("1. Curier Rapid (Standard)");
            Console.WriteLine("2. Ridicare Personală (Punct de ridicare)");
            Console.Write("Alegeți metoda de livrare: ");

            string opt = Console.ReadLine();
            string metoda = (opt == "1") ? "Curier Rapid" : "Punct Ridicare";

            Console.WriteLine($"\n[LIVRARE] Comanda #{order.OrderId} va fi trimisă prin {metoda}.");
        }
    }
}
using System;
using System.Collections.Generic;

namespace EStore
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Catalog de produse diversificat
            List<Produs> catalog = new List<Produs>
            {
                new Produs { Id = 1, Name = "Laptop ASUS Rog", Price = 4500, Category = "Laptop" },
                new Produs { Id = 2, Name = "Mouse Logitech G502", Price = 350, Category = "Accesorii" },
                new Produs { Id = 3, Name = "Tastatura Mecanica Razer", Price = 600, Category = "Accesorii" },
                new Produs { Id = 4, Name = "Monitor Dell 27 inch", Price = 1200, Category = "Monitoare" },
                new Produs { Id = 5, Name = "Casti HyperX Cloud II", Price = 400, Category = "Audio" },
                new Produs { Id = 6, Name = "Scaun Gaming SecretLab", Price = 2200, Category = "Furniture" }
            };

            // 2. Afisare Catalog pentru utilizator
            Console.WriteLine("=== CATALOG PRODUSE E-STORE ===");
            foreach (var p in catalog) Console.WriteLine(p);
            Console.WriteLine("===============================\n");

            // 3. Creare Client
            Customer client = new Customer
            {
                Username = "Andrei_Student",
                Email = "andrei@upt.ro",
                ShippingAddress = "Complex Studentesc, Timisoara"
            };

            // 4. Shopping Cart - Clientul adaugă produse
            ShoppingCart cart = new ShoppingCart();
            cart.AddProduct(catalog[0]); // Laptop
            cart.AddProduct(catalog[2]); // Tastatura
            cart.AddProduct(catalog[4]); // Casti

            // 5. Alegerea Campaniei de Discount (Strategia)
            // Aici poți schimba cu StudentDiscount() sau NoDiscount()
            IDiscountStrategy campanieActiva = new BlackFridayDiscount();

            // 6. Creare si Procesare Comanda
            Order comanda = new Order(
                1001,
                client,
                cart,
                new CardPayment(),
                new FanCourier(),
                campanieActiva
            );

            client.DisplayMenu();
            comanda.ProcessOrder();

            // 7. Simulare Admin care vede comanda
            Admin admin = new Admin { Username = "Sef_Depozit" };
            admin.ManageOrder(comanda);

            Console.WriteLine("\nApăsați orice tastă pentru a ieși...");
            Console.ReadKey();
        }
    }
}
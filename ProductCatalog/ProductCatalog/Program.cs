using System;
using System.Linq;

namespace ProductCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IProductRepository repo = new ProductRepository();
            ProductFilterService filter = new ProductFilterService();
            ProductPresentationService presenter = new ProductPresentationService();
            IPaymentMethod paymentProcessor = new PaymentService(); 
            IShippingProvider shippingProcessor = new ShippingService(); 

            Cart myCart = new Cart();

          
            StoreManager store = new StoreManager(repo, filter, presenter, paymentProcessor, shippingProcessor);

            Console.WriteLine("===========================================");
            Console.WriteLine("   SISTEM GESTIUNE MAGAZIN ONLINE v2.0    ");
            Console.WriteLine("===========================================");

         
            Console.Write("\nIntroduceti numele dumneavoastră: ");
            string userName = Console.ReadLine();

            Console.Write("Sunteti administrator? (DA/NU): ");
            UserRole role = Console.ReadLine().Trim().ToUpper() == "DA" ? UserRole.Admin : UserRole.Client;

            User currentUser = new User { Name = userName, Role = role };

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"\n-------------------------------------------");
                Console.WriteLine($" UTILIZATOR: {currentUser.Name} | ROL: {currentUser.Role}");
                Console.WriteLine($" COȘ: {myCart.Content.Count} produse | TOTAL: {myCart.GetTotal()} RON");
                Console.WriteLine($"-------------------------------------------");
                Console.WriteLine("1. Vizualizare si Filtrare Catalog ");
                Console.WriteLine("2. Adaugă Produs in Cos ");
                Console.WriteLine("3. FINALIZARE COMANDA ");

                if (currentUser.Role == UserRole.Admin)
                {
                    Console.WriteLine("4. [ADMIN] Adaugare Produs Nou");
                }

                Console.WriteLine("5. Iesire");
                Console.Write("\nSelectati o optiune: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n--- CONFIGURARE CAUTARE ---");
                        Console.Write("Filtru Categorie (Enter pentru toate): ");
                        string cat = Console.ReadLine();

                        Console.Write("Pret Maxim (Enter pentru fara limita): ");
                        string pInput = Console.ReadLine();
                        decimal? maxP = string.IsNullOrEmpty(pInput) ? null : decimal.Parse(pInput);

                        Console.WriteLine("Sortare:");
                        Console.WriteLine("  1. Pret Crescator");
                        Console.WriteLine("  2. Pret Descrescator");
                        Console.WriteLine("  3. Alfabetic A - Z");
                        Console.WriteLine("  4. Alfabetic Z - A");
                        Console.Write("Alegeti sortare (1/2/3/4): ");
                        string sort = Console.ReadLine();

                       
                        store.Search(cat, maxP, sort);
                        break;

                    case "2":
                        Console.Write("\nIntroduceti ID-ul produsului pentru cos: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            
                            var product = repo.GetProducts().FirstOrDefault(p => p.Id == id);
                            if (product != null)
                            {
                                myCart.Add(product);
                                Console.WriteLine($"[OK] {product.Name} a fost adaugat în cos.");
                            }
                            else
                            {
                                Console.WriteLine("[!] Eroare: Produsul cu acest ID nu exista.");
                            }
                        }
                        break;

                    case "3":
                        
                        store.Checkout(myCart);
                        Console.WriteLine("\nApasati orice tasta pentru a reveni la meniu...");
                        Console.ReadKey();
                        break;

                    case "4":
                        if (currentUser.Role == UserRole.Admin)
                        {
                            Console.WriteLine("\n--- ADAUGARE PRODUS NOU ---");
                            Console.Write("Nume: "); string n = Console.ReadLine();
                            Console.Write("Categorie: "); string c = Console.ReadLine();
                            Console.Write("Preț: "); decimal pr = decimal.Parse(Console.ReadLine());

                            store.AdminAdd(currentUser, new Product
                            {
                                Id = new Random().Next(100, 999),
                                Name = n,
                                Category = c,
                                Price = pr
                            });
                        }
                        else
                        {
                            Console.WriteLine("[!] Acces interzis. Nu aveti drepturi de administrator.");
                        }
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Va mulțumim! O zi buna!");
                        break;

                    default:
                        Console.WriteLine("[!] Optiune invalida.");
                        break;
                }
            }
        }
    }
}
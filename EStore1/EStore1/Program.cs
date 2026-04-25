using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EStore;

namespace EStore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var db = new EStoreContext();
            await db.Database.EnsureCreatedAsync(); // Creează DB dacă nu există

            var repo = new ProductRepository(db); // Folosim Repository-ul creat mai sus

            while (true)
            {
                Console.WriteLine("\n=== MAGAZIN EF CORE ===");
                Console.WriteLine("1. CLIENT | 2. ADMIN | 0. Ieșire");
                string? opt = Console.ReadLine();
                if (opt == "0") break;

                if (opt == "2") await MeniuAdmin(db, repo);
                else await MeniuClient(db, repo);
            }
        }

        static async Task MeniuClient(EStoreContext db, IProductRepository repo)
        {
            // AICI declarăm variabila produse
            List<Produs> catalog = await repo.GetAllAsync();

            Console.WriteLine("\n--- CATALOG ---");
            foreach (var p in catalog) Console.WriteLine($"{p.Id}. {p.Name} - {p.Price} RON");

            var cos = new ShoppingCart();
            while (true)
            {
                Console.Write("ID produs (0 pt Checkout): ");
                if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) break;

                // Folosim variabila 'catalog' declarată mai sus
                var p = catalog.FirstOrDefault(x => x.Id == id);
                if (p != null)
                {
                    cos.ProduseInCos.Add(p);
                    Console.WriteLine($"Adăugat: {p.Name}");
                }
            }

            if (cos.ProduseInCos.Any())
            {
                using var transaction = await db.Database.BeginTransactionAsync();
                try
                {
                    var o = new Order
                    {
                        OrderId = "CMD-" + Random.Shared.Next(100, 999),
                        CustomerName = "Client_EF",
                        PretFinal = cos.ProduseInCos.Sum(x => x.Price)
                    };
                    db.Comenzi.Add(o);
                    await db.SaveChangesAsync();

                    foreach (var p in cos.ProduseInCos)
                        db.OrderItems.Add(new OrderItem { OrderId = o.Id, ProdusId = p.Id });

                    await db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Console.WriteLine("Comandă salvată cu succes!");
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        static async Task MeniuAdmin(EStoreContext db, IProductRepository repo)
        {
            Console.WriteLine("1. Adaugă Produs | 2. Vezi Comenzi");
            string? opt = Console.ReadLine();
            if (opt == "1")
            {
                Console.Write("Nume: "); string n = Console.ReadLine() ?? "";
                Console.Write("Pret: "); double p = double.Parse(Console.ReadLine() ?? "0");
                await repo.AddAsync(new Produs { Name = n, Price = p });
            }
            else
            {
                var comenzi = await db.Comenzi.Include(c => c.Items).ThenInclude(i => i.Produs).ToListAsync();
                foreach (var c in comenzi) Console.WriteLine($"Comanda {c.OrderId} | Produse: {c.Items.Count}");
            }
        }
    }
}
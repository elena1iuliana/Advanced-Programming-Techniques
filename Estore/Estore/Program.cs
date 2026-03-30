using Estore;
using var db = new EStoreContext();
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();
IProductRepository repo = new ProductRepository(db);
await repo.SeedInitialDataAsync();
var logger = new ConsoleLogger();
var orderService = new OrderService(logger);
List<Product> cart = new();
while (true)
{
    Console.WriteLine("\n=== MAGAZIN ONLINE ===");
    Console.WriteLine("1. Catalog Produse");
    Console.WriteLine("2. Checkout (Finalizare Comanda)");
    Console.WriteLine("0. Iesire");
    Console.Write("Selectati optiunea: ");

    string choice = Console.ReadLine() ?? "0";

    if (choice == "0") break;

    if (choice == "1")
    {
        var products = await repo.GetAllAsync();
        Console.WriteLine("\n--- Catalog Produse ---");
        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id}. {p.Name} - {p.Price} RON");
        }

        Console.Write("\nIntroduceti ID-ul produsului pentru a-l adauga in cos: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var selectedProduct = products.FirstOrDefault(x => x.Id == id);
            if (selectedProduct != null)
            {
                cart.Add(selectedProduct);
                Console.WriteLine($"[OK] {selectedProduct.Name} a fost adaugat in cos.");
            }
            else
            {
                Console.WriteLine("[Eroare] ID-ul introdus nu exista!");
            }
        }
    }

    if (choice == "2")
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("[Info] Cosul este gol. Adaugati produse inainte de checkout.");
            continue;
        }

        await orderService.ProcessCheckout(cart, db);
    }
}
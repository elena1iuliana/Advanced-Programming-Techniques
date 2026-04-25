using var db = new LibraryContext();

db.Database.EnsureCreated();

var service = new LibraryService(db);

Console.WriteLine("=== AUTENTIFICARE BIBLIOTECA ===");
Console.Write("Prenume: "); string p = Console.ReadLine()!;
Console.Write("Nume: "); string n = Console.ReadLine()!;
User currentUser = service.GetOrCreateUser(p, n);

bool exit = false;
while (!exit)
{
    Console.WriteLine($"\n--- SESIUNE: {currentUser.FirstName.ToUpper()} (ID: {currentUser.Id}) ---");
    Console.WriteLine("1. Adauga Carte | 2. Vezi Toate | 3. Imprumuta | 4. Cauta Titlu");
    Console.WriteLine("5. Vezi Disponibile | 6. Statistici Genuri | 0. Iesire");
    Console.Write("Alege: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Titlu: "); string t = Console.ReadLine()!;
            Console.Write("Autor: "); string a = Console.ReadLine()!;
            Console.Write("Gen: "); string g = Console.ReadLine()!;
            service.AddBook(t, a, g);
            break;
        case "2":
            service.GetAllBooks().ForEach(b => Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} ({(b.IsBorrowed ? "Imprumutata" : "Libera")})"));
            break;
        case "3":
            Console.Write("ID Carte: "); int id = int.Parse(Console.ReadLine()!);
            if (service.BorrowBook(id, currentUser.Id)) Console.WriteLine("Succes!");
            else Console.WriteLine("Indisponibila!");
            break;
        case "4":
            Console.Write("Cauta: "); string term = Console.ReadLine()!;
            service.Search(term).ForEach(b => Console.WriteLine($"- {b.Title}"));
            break;
        case "5":
            service.GetAvailableBooks().ForEach(b => Console.WriteLine($"- {b.Title}"));
            break;
        case "6":
            foreach (var item in service.GetStatistics()) Console.WriteLine($"{item.Genre}: {item.Count} carti");
            break;
        case "0": exit = true; break;
    }
}
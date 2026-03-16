using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LibraryTracker
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            IsBorrowed = false;
        }

        public override string ToString()
        {

            string status = IsBorrowed ? "Împrumutată" : "Disponibilă";
            return $"[{status}] {Title} - {Author}";
        }
    }

    class Program
    {
        static List<Book> library = new List<Book>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Library Tracker ---");
                Console.WriteLine("1. Adaugă o carte");
                Console.WriteLine("2. Vezi lista de cărți");
                Console.WriteLine("3. Împrumută o carte");
                Console.WriteLine("4. Ieșire");
                Console.Write("Alege o opțiune: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ShowBooks();
                        break;
                    case "3":
                        BorrowBook();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opțiune invalidă!");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Titlu: ");
            string title = Console.ReadLine();
            Console.Write("Autor: ");
            string author = Console.ReadLine();

            library.Add(new Book(title, author));
            Console.WriteLine("Cartea a fost adăugată cu succes!");
        }

        static void ShowBooks()
        {
            Console.WriteLine("\n--- Lista de Cărți ---");
            if (library.Count == 0) Console.WriteLine("Biblioteca este goală.");

            for (int i = 0; i < library.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {library[i]}");
            }
        }

        static void BorrowBook()
        {
            ShowBooks();
            if (library.Count == 0) return;

            Console.Write("Introdu numărul cărții pe care vrei să o împrumuți: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= library.Count)
            {
                if (!library[index - 1].IsBorrowed)
                {
                    library[index - 1].IsBorrowed = true;
                    Console.WriteLine($"Ai împrumutat: {library[index - 1].Title}");
                }
                else
                {
                    Console.WriteLine("Această carte este deja împrumutată!");
                }
            }
            else
            {
                Console.WriteLine("Număr invalid.");
            }
        }
    }
}
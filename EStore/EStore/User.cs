using System;

namespace EStore
{
    // Clasa de bază pentru toți utilizatorii
    public abstract class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public abstract void DisplayMenu();
    }

    // Clientul - cel care cumpără
    public class Customer : User
    {
        public string ShippingAddress { get; set; }
        public override void DisplayMenu() =>
            Console.WriteLine($"\n--- Menu pentru Clientul: {Username} ---");
    }

    // Administratorul - cel care gestionează (AICI lipsea metoda!)
    public class Admin : User
    {
        public override void DisplayMenu() =>
            Console.WriteLine($"\n--- Menu Administrator: Gestionare Stocuri ---");

        // ACEASTA ESTE METODA CARE REZOLVĂ EROAREA
        // Ea primește obiectul 'order' creat în Program.cs
        public void ManageOrder(Order order)
        {
            Console.WriteLine($"\n[ADMIN] Verificare comandă ID: {order.OrderId}");
            Console.WriteLine($"[ADMIN] Status actual: {order.Status}");
            Console.WriteLine($"[ADMIN] Administratorul {Username} a aprobat expedierea.");
        }
    }
}
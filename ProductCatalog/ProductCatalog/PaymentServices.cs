using System;

namespace ProductCatalog
{
    public interface IPaymentMethod
    {
        void Process(decimal amount);
    }

    public class PaymentService : IPaymentMethod
    {
        public void Process(decimal amount)
        {
            Console.WriteLine("\n--- OPȚIUNI DE PLATĂ ---");
            Console.WriteLine("1. Card Online");
            Console.WriteLine("2. Cash (Ramburs la livrare)");
            Console.Write("Selectați metoda (1/2): ");

            string opt = Console.ReadLine();

            if (opt == "1")
            {
                Console.WriteLine("\n[PLATĂ CARD]");
                Console.Write("Introduceți numărul cardului (simbolic): ");
                Console.ReadLine();
                Console.WriteLine($"[SUCCES] Suma de {amount} RON a fost procesată cu succes.");
            }
            else
            {
                Console.WriteLine("\n[PLATĂ CASH]");
                Console.WriteLine($"[NOTĂ] Veți achita suma de {amount} RON direct curierului la livrare.");
            }
        }
    }
}
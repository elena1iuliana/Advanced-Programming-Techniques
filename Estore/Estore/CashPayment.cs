using Estore;

public class CashPayment : IPaymentStrategy
{
    public string GetName() => "Cash (Ramburs)";
    public void Process(double amount) => Console.WriteLine($"Suma de {amount} RON va fi achitata la livrare.");
}
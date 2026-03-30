using Estore;

public class CardPayment : IPaymentStrategy
{
    public string GetName() => "Card Bancar";
    public void Process(double amount) => Console.WriteLine($"Plata de {amount} RON procesata prin Terminal Card.");
}
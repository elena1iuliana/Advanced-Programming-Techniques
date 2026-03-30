namespace Estore;

public interface IPaymentStrategy
{
    string GetName();
    void Process(double amount);
}


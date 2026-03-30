namespace Estore;

public interface IShippingStrategy
{
    string GetName();
    double GetCost();
}


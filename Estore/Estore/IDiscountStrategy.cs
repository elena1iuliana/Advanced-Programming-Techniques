namespace Estore;

public interface IDiscountStrategy
{
    string GetDescription();
    double Calculate(double total);
}


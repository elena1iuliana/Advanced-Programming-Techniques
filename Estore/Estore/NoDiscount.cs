namespace Estore;

public class NoDiscount : IDiscountStrategy
{
    public string GetDescription() => "Fara Discount";
    public double Calculate(double total) => total;
}
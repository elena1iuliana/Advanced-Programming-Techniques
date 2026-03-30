namespace Estore;

public class SeniorDiscount : IDiscountStrategy
{
    public string GetDescription() => "Pensionar (20%)";
    public double Calculate(double total) => total * 0.80;
}
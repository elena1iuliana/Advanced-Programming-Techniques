using Estore;

public class Sameday : IShippingStrategy
{
    public string GetName() => "Sameday (Easybox)";
    public double GetCost() => 12.0;
}
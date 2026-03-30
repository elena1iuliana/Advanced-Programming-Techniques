namespace Estore;

public class FanCourier : IShippingStrategy
{
    public string GetName() => "Fan Courier";
    public double GetCost() => 20.0;
}
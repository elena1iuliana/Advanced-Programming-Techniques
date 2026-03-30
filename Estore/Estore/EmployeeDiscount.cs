namespace Estore;

public class EmployeeDiscount : IDiscountStrategy
{
    public string GetDescription() => "Angajat (25%)";
    public double Calculate(double total) => total * 0.75;
}
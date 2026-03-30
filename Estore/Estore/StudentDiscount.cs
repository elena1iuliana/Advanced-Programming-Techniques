namespace Estore;

public class StudentDiscount : IDiscountStrategy
{
    public string GetDescription() => "Student (15%)";
    public double Calculate(double total) => total * 0.85;
}
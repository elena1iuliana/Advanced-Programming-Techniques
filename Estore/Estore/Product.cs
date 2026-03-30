namespace Estore;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }


    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    private Product() { Name = ""; }
}
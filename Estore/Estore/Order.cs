namespace Estore;

public class Order
{
    public int Id { get; private set; }
    public string Code { get; private set; }
    public double TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    public Order(string code, double total)
    {
        Code = code;
        TotalAmount = total;
        Status = OrderStatus.New;
    }

    private Order() { Code = ""; } 
}

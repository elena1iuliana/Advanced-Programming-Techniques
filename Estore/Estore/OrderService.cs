using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Estore;

public class OrderService
{
    private readonly ILogger _logger;

    public OrderService(ILogger logger)
    {
        _logger = logger;
    }
    public async Task ProcessCheckout(List<Product> cart, EStoreContext db)
    {
        if (cart == null || !cart.Any())
        {
            Console.WriteLine("\n[!] Cosul este gol. Nu se poate efectua plata.");
            return;
        }
        Console.WriteLine("\n--- DETALII COS CURENT ---");
        foreach (var item in cart)
        {
            Console.WriteLine($"- {item.Name}: {item.Price} RON");
        }
        double subtotal = cart.Sum(p => p.Price);
        Console.WriteLine($"---------------------------");
        Console.WriteLine($"Subtotal produse: {subtotal} RON");
        var discount = GetDiscountSelection();
        var shipping = GetShippingSelection();
        var payment = GetPaymentSelection();
        double pretDupaDiscount = discount.Calculate(subtotal);
        double costTransport = shipping.GetCost();
        double totalFinal = pretDupaDiscount + costTransport;
        Console.WriteLine("\n--- REZUMAT PLATA ---");
        Console.WriteLine($"Descriere Discount: {discount.GetDescription()}");
        Console.WriteLine($"Pret dupa reducere: {pretDupaDiscount} RON");
        Console.WriteLine($"Cost Livrare ({shipping.GetName()}): {costTransport} RON");
        Console.WriteLine($"TOTAL DE ACHITAT: {totalFinal} RON");
        Console.WriteLine("---------------------\n");
        _logger.Log($"Se incearca procesarea platii de {totalFinal} RON...");
        using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            payment.Process(totalFinal);
            var order = new Order($"ORD-{Guid.NewGuid().ToString()[..8]}", totalFinal);
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            Console.WriteLine($"\n[SUCCESS] Comanda {order.Code} a fost finalizata cu succes!");
            cart.Clear();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.Log($"Eroare la procesarea comenzii: {ex.Message}");
            Console.WriteLine("\n[ERROR] A aparut o problema la plata. Reincercati.");
        }
    }
    private IDiscountStrategy GetDiscountSelection()
    {
        Console.WriteLine("\nAlegeti Discount: 1.Student (15%) | 2.Angajat (25%) | 3.Pensionar (20%) | 4.Fara");
        return Console.ReadLine() switch
        {
            "1" => new StudentDiscount(),
            "2" => new EmployeeDiscount(),
            "3" => new SeniorDiscount(),
            _ => new NoDiscount()
        };
    }
    private IShippingStrategy GetShippingSelection()
    {
        Console.WriteLine("Alegeti Curier: 1.FanCourier (20 RON) | 2.Sameday (12 RON)");
        return Console.ReadLine() == "1" ? new FanCourier() : new Sameday();
    }
    private IPaymentStrategy GetPaymentSelection()
    {
        Console.WriteLine("Metoda Plata: 1.Card Bancar | 2.Cash la livrare");
        return Console.ReadLine() == "1" ? new CardPayment() : new CashPayment();
    }
}
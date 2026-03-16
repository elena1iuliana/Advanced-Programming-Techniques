using System;
using System.Collections.Generic;
using System.Text;

namespace EStore;
   public interface IPaymentStrategy { void Pay(double amount); }
    public interface IShippingStrategy { void Ship(string address); }
    public interface IDiscountStrategy {
    string CampaignName { get; }
    double ApplyDiscount(double price); }

    // Implementări concrete
    public class CardPayment : IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"[Plată] S-a achitat suma de {amount} RON prin Card Bancar.");
    }

    public class FanCourier : IShippingStrategy
    {
        public void Ship(string address) => Console.WriteLine($"[Shipping] Expediere prin FanCourier la adresa: {address}");
    }

public class BlackFridayDiscount : IDiscountStrategy
{
    public string CampaignName => "Black Friday - 20% OFF";
    public double ApplyDiscount(double price) => price * 0.8;
}

// Campania 2: Reducere de Student (15% reducere)
public class StudentDiscount : IDiscountStrategy
{
    public string CampaignName => "Student Promo - 15% OFF";
    public double ApplyDiscount(double price) => price * 0.85;
}

// Campania 3: Fără discount
public class NoDiscount : IDiscountStrategy
{
    public string CampaignName => "Pret Intreg";
    public double ApplyDiscount(double price) => price;
}


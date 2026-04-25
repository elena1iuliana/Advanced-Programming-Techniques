using System;

namespace EStore
{
    public interface IDiscountStrategy
    {
        string CampaignName { get; }
        double ApplyDiscount(double price);
    }

    public class BlackFridayDiscount : IDiscountStrategy
    {
        public string CampaignName => "Black Friday (20%)";
        public double ApplyDiscount(double price) => price * 0.8;
    }

    public class NoDiscount : IDiscountStrategy
    {
        public string CampaignName => "Pret Intreg";
        public double ApplyDiscount(double price) => price;
    }
}
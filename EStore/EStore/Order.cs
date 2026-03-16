using System;
using System.Collections.Generic;

namespace EStore
{
    public enum OrderStatus { Plasata, InProcesare, Expediata, Livrata }

    public class Order
    {
        public int OrderId { get; set; } // Acum poate fi setat din constructor
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; set; }

        private Customer _customer;
        private ShoppingCart _cart;
        private List<Produs> _orderedItems;

        private IPaymentStrategy _payment;
        private IShippingStrategy _shipping;
        private IDiscountStrategy _discount;

        // CONSTRUCTORUL CU 6 ARGUMENTE (Sincronizat cu Program.cs)
        public Order(int id, Customer customer, ShoppingCart cart, IPaymentStrategy payment, IShippingStrategy shipping, IDiscountStrategy discount)
        {
            OrderId = id;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Plasata;

            _customer = customer;
            _cart = cart;
            _orderedItems = new List<Produs>(cart.GetItems());

            _payment = payment;
            _shipping = shipping;
            _discount = discount;
        }

        public void ProcessOrder()
        {
            if (_orderedItems.Count == 0) return;

            double initialTotal = _cart.CalculateTotal();
            double finalTotal = _discount.ApplyDiscount(initialTotal);

            Console.WriteLine($"\n--- PROCESARE COMANDĂ: {OrderId} ---");
            Console.WriteLine($"Total brut: {initialTotal} RON | Total după {_discount.CampaignName}: {finalTotal} RON");

            _payment.Pay(finalTotal);
            _shipping.Ship(_customer.ShippingAddress);

            Status = OrderStatus.InProcesare;
        }
    }
}
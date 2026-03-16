using System;
using System.Linq;
using System.Collections.Generic;

namespace ProductCatalog
{
    public class StoreManager
    {
        
        private readonly IProductRepository _repo;
        private readonly ProductFilterService _filter;
        private readonly ProductPresentationService _presenter;
        private readonly IPaymentMethod _payment;
        private readonly IShippingProvider _shipping;

       
        public StoreManager(
            IProductRepository repo,
            ProductFilterService filter,
            ProductPresentationService presenter,
            IPaymentMethod payment,
            IShippingProvider shipping)
        {
            _repo = repo;
            _filter = filter;
            _presenter = presenter;
            _payment = payment;
            _shipping = shipping;
        }

        
        public void Search(string cat, decimal? max, string sortOrder)
        {
            var query = _repo.GetProducts();

         
            var filteredQuery = _filter.ApplyCriteria(query, cat, max, sortOrder);

            
            _presenter.DisplayGrouped(filteredQuery);
        }

       
        public void Checkout(Cart cart)
        {
            if (!cart.Content.Any())
            {
                Console.WriteLine("\n[!] Coșul este gol!");
                return;
            }

            var order = new Order
            {
                OrderId = new Random().Next(1000, 9999),
                Items = new List<Product>(cart.Content),
                TotalAmount = cart.GetTotal()
            };

           
            _payment.Process(order.TotalAmount);

            _shipping.Deliver(order);

            Console.WriteLine("\n=== TRANZACȚIE FINALIZATĂ CU SUCCES ===");
            cart.Content.Clear();
        }

     
        public void AdminAdd(User u, Product p)
        {
            if (u.Role != UserRole.Admin)
            {
                Console.WriteLine("\n!!! ACCES REFUZAT: Doar Adminul poate adăuga produse.");
                return;
            }
            _repo.Add(p);
            Console.WriteLine($"\n[ADMIN] {u.Name} a adăugat produsul: {p.Name}");
        }
    }
}
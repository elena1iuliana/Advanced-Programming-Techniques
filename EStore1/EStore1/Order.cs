using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EStore
{
    public enum StatusComanda { InAsteptare, Expediata, Anulata }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public double PretFinal { get; set; }
        public StatusComanda Status { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProdusId { get; set; }
        public Produs? Produs { get; set; }
    }
}
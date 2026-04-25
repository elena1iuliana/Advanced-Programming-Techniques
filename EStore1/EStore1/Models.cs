using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EStore
{
    public class EStoreContext : DbContext
    {
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Order> Comenzi { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EStoreDB;Trusted_Connection=True;");
    }

    public class Produs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }

    // Această clasă rămâne în memorie pentru sesiunea curentă
    public class ShoppingCart
    {
        public List<Produs> ProduseInCos { get; set; } = new();
    }
}
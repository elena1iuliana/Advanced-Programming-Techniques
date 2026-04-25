using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EStore
{
    public class EStoreContext : DbContext
    {
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Order> Comenzi { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Această linie configurează conexiunea către baza de date locală
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EStoreDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cerința: Model configurations
            modelBuilder.Entity<Produs>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Order>().HasMany(o => o.Items).WithOne().HasForeignKey(i => i.OrderId);
        }
    }
}
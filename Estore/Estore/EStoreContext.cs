using Microsoft.EntityFrameworkCore;

namespace Estore;

public class EStoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EStore_Final_DB;Trusted_Connection=True;TrustServerCertificate=True;");
}
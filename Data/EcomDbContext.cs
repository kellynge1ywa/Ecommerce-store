using Microsoft.EntityFrameworkCore;

namespace Ecommerce;

public class EcomDbContext:DbContext
{

    public EcomDbContext(DbContextOptions<EcomDbContext>options): base(options)
    {
        
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     // optionsBuilder.UseSqlServer(myC);
    // }
    public DbSet<Product> Products {get;set;}
    public DbSet<Order> Orders {get;set;}
    public DbSet<OrderProducts> OrderProducts {get;set;}

    public DbSet<User> Users {get;set;}

}

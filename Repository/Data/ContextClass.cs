using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System.Diagnostics;


namespace Repository.Data
{
    public class ContextClass : DbContext
    {

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<OrderDetail> Orderdetails { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {  //Verbindungsschlüssel
            ConnectionString conString = new ConnectionString();

            optionsBuilder.UseSqlServer(conString.getConnectionString());
        }

    }
}

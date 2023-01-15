using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Repository.Models;


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
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=TestDatabase;Integrated Security=True");
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System.Data.SqlTypes;
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
            optionsBuilder.UseSqlServer(ConnectionString());
        }

        public string ConnectionString()
        {
            string strAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var values = strAssemblyPath.Split(@"\bin");
            string fileName = values[0] + @"\Database\ConnectionString.txt";

            try
            {
                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        return s;
                    }
                        return s;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return "";
            }
        }

    }
}

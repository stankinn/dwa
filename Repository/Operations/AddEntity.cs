using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;
using Repository.Models;
using System.Windows;

 

namespace Repository.Operations
{
    public class AddEntity
    {
        ContextClass context = new ContextClass();
        public void add(string name, decimal price)
        {
        Product product = new Product()
        {
            Name = name,
            Price = price
        };
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void addAll(string name, decimal price, int cal, int dia)
        {
            Product product = new Product()
            {
                Name = name,
                Price = price,
                Calories = cal,
                Diameter = dia
            };
            
            if (cal > 0 && dia > 0 && price > 0) 
            { 
            context.Products.Add(product);
            context.SaveChanges();
            }

        }


    }
}

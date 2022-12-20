using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;
using Repository.Models;


namespace Repository.Operations
{
    public class DeleteEntity
    {

        ContextClass context = new ContextClass();

        public void deleteByName(string name)
        {

            var product = context.Products
                .Where(p => p.Name == name)
                .First();

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void deleteByPrice(decimal price)
        {
            var product = context.Products
                .Where(p => p.Price == price)
                .First();

            context.Products.Remove(product);
            context.SaveChanges();
        }

        public void deleteById(int id)
        {
            var product = context.Products
                .Where(p => p.Id == id)
                .First();

            context.Products.Remove(product);
            context.SaveChanges();
        }



    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;
using Repository.Models;



namespace Repository.Operations
{
    public class AlterEntity
    {
        ContextClass context = new ContextClass();

        
        //checks if entry is already existent by name
        public bool doesExist(string name)
        {

            if (!context.Products.Any(p => p.Name == name))
            {
                return false;
            }

            return true;
                    
        }


        //alter name
        public void alterRename(string name, string newName)
        {

            var product = context.Products
                .Where(p => p.Name == name )
                .FirstOrDefault();
                              
            if (product is Product)
            {
                product.Name = newName;
            }

            context.SaveChanges();
        }

        //alter price based on name
        public void alterChangePrice(string name, decimal price)
        {

            var product = context.Products
                .Where(p => p.Name == name)
                .FirstOrDefault();

            if (product is Product)
            {
                product.Price = price;
            }

            context.SaveChanges();
        }

        //change calories
        public void alterChangeCalories(string name, int cal)
        {
            var product = context.Products
                .Where(p => p.Name == name)
                .FirstOrDefault();

            if(product is Product)
            {
                product.Calories = cal;
            }

            context.SaveChanges();
        }

        public void alterChangeDiameter(string name, int dia)
        {
            var product = context.Products
                .Where(p => p.Name == name)
                .FirstOrDefault();

            if (product is Product)
            {
                product.Calories = dia;
            }

            context.SaveChanges();
        }

    }
}

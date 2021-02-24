using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    //Caisse enregistreuse
    class CashRegister
    {
        List<Product> products;
        List<Order> orders;

        public List<Product> Products { get => products; set => products = value; }
        public List<Order> Orders { get => orders; set => orders = value; }

        public CashRegister()
        {
            Products = new List<Product>();
            orders = new List<Order>();
        }

        public Product CreateProduct(string title, decimal price, int stock)
        {
            //Vérification sur les champs ....
            Product product = new Product() { Title = title, Price = price, Stock = stock };

            if (product.save() > 0)
            {
                Products.Add(product);
                return product;
            }

            return null;
            
        }

        public bool AddOrder(Order order)
        {
            if (order.save() > 0)
            {
                Orders.Add(order);
                return true;
            }

            return false;
        }

        public Product SearchProductById(int id)
        {
            //Effectuer une recherche de produit avec une expression lambda et la méthode find des listes
            return Products.Find(p => p.Id == id);
        }
    }
}

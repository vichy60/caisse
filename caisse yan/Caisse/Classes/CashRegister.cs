using Caisse.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Caisse.Classes
{
    //Caisse enregistreuse
    class CashRegister
    {
        List<Product> products;
        List<Order> orders;
        private static SqlCommand command;
        private static SqlDataReader reader;

        public List<Product> Products { get => products; set => products = value; }
        public List<Order> Orders { get => orders; set => orders = value; }

        public CashRegister()
        {
            Products = new List<Product>();
            orders = new List<Order>();
            Products = loadProducts();
        }

        public Product CreateProduct(string title, decimal price, int stock)
        {
            //Vérification sur les champs ....
            Product product = new Product() { Title = title, Price = price, Stock = stock };

            if (product.Save())
            {
                Products.Add(product);
                return product;
            }

            return null;



        }


        public List<Product> loadProducts()
        {

            List<Product> products = new List<Product>();
            //Requete save product
            string request = "SELECT title,price,stock,id FROM  product";
            command = new SqlCommand(request, DataBase.Connection);

            DataBase.Connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Product p = new Product() { Title = reader.GetString(0), Price = reader.GetDecimal(1), Stock = reader.GetByte(2),Id=reader.GetInt32(3) };
                products.Add(p);


            }
            reader.Close();
            command.Dispose();
            DataBase.Connection.Close();

            return products;

        }






        public bool AddOrder(Order order)
        {
            Orders.Add(order);
            return true;
        }

        public Product SearchProductById(int id)
        {
            //Effectuer une recherche de produit avec une expression lambda et la méthode find des listes
            return Products.Find(p => p.Id == id);
        }
    }
}

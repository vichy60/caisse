using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Caisse.Tools;



namespace Caisse.Classes
{
    class Product
    {
        private int id;
        private string title;
        private decimal price;
        private int stock;
        private static int count = 0;
        private static SqlCommand command;
        private static SqlDataReader reader;

        public int Id { get => id;}
        public string Title { get => title; set => title = value; }
        public decimal Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
    

        public Product()
        {
            id = ++count;
        }

        public bool Save()
        {
            //Requete save product
            string request = "INSERT INTO product (title, price, stock) OUTPUT INSERTED.ID values (@title, @price, @stock)";
            command = new SqlCommand(request, DataBase.Connection);
          
            return Id > 0;
        }




        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}

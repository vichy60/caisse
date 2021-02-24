using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Caisse.Tools;

namespace Caisse.Classes
{
    class Product
    {
        private bool availble;
        private bool archivedProduct;
        private int id;
        private string title;
        private decimal price;
        private int stock;

        private static bool distantBD = false;
        private static int count = 0;

        /* ****************** */
        private static SqlCommand command;
        private static SqlDataReader reader;
        /* ****************** */
        public int Id { get => id;}
        public string Title { get => title; set => title = value; }
        public decimal Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
        /* ****************** */
        public Product()
        {
            id = -1;

        }


        public int save()
        {
            //Requete save product
            string request = "INSERT INTO product (title, price, stock) OUTPUT INSERTED.ID values (@title, @price, @stock)";
            command = new SqlCommand(request, DataBase.Connection);


            command.Parameters.Add(new SqlParameter("title", title));
            command.Parameters.Add(new SqlParameter("price", price));
            command.Parameters.Add(new SqlParameter("stock", stock));


            object result = command.ExecuteScalar();
            
            if ((result.GetType() != typeof(DBNull)) && int.TryParse(result.ToString(), out this.id) ) {
                // inserted ID
                command.Dispose();
                return Id;
            } else {
                command.Dispose();
                return -1;
            }

        }



        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}

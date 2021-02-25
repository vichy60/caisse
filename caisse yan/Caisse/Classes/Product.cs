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

        public int Id { get => id; set => id = value; }
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
            command.Parameters.Add(new SqlParameter("@title", Title));
            command.Parameters.Add(new SqlParameter("@price", Price));
            command.Parameters.Add(new SqlParameter("@stock", Stock));
            DataBase.Connection.Open();
            Id = (int)command.ExecuteScalar();
            command.Dispose();
            DataBase.Connection.Close();
            return Id > 0;


        }


        public bool UpdateStock()
        {
            string request = "UPDATE product SET stock= @stock WHERE id=@id";

            command = new SqlCommand(request, DataBase.Connection);
            command.Parameters.Add(new SqlParameter("@stock", Stock));
            command.Parameters.Add(new SqlParameter("@id", Id));

            DataBase.Connection.Open();
            int nb=command.ExecuteNonQuery();
            command.Dispose();
            DataBase.Connection.Close();
            return nb>0;



        }




















        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}

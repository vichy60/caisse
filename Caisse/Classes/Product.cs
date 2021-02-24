using System;
using System.Collections.Generic;
using System.Text;

namespace Caisse.Classes
{
    class Product
    {
        private int id;
        private string title;
        private decimal price;
        private int price;
        private static int count = 0;

        public int Id { get => id;}
        public string Title { get => title; set => title = value; }
        public decimal Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
    

        public Product()
        {
            id = ++count;
        }


        public int save()
        {
            string requete = @"insert into Product (title, price, stock ) values (@title, @price, @stock) ";

            

            if (command) {
                // inserted ID
                return Id;
            } else {
                return 0;
            }

        }



        public override string ToString()
        {
            return $"Id : {Id}, Titre : {Title}, Prix : {Price}";
        }
    }
}

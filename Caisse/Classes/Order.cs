using Caisse.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Caisse.Tools;

namespace Caisse.Classes
{
    //=> classe vente
    class Order
    {
        //Reference unique de la vente
        private int id;
        //Date et heure de la vente
        private DateTime dateOrder;
        private List<Product> products;
        private OrderStatus status;
        private IPayment payment;

        private static int count = 0;


        /* ****************** */
        private static SqlCommand command;
        private static SqlDataReader reader;
        /* ****************** */

        public int Id { get => id; }
        public DateTime DateOrder { get => dateOrder; set => dateOrder = value; }
        public List<Product> Products { get => products; set => products = value; }
        public OrderStatus Status { get => status; set => status = value; }
        public IPayment Payment { get => payment; set => payment = value; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                //Expression lambda pour parccourir une liste
                Products.ForEach(p =>
                {
                    total += p.Price;
                });
                return total;
            }
        }

        public Order()
        {
            Products = new List<Product>();
            //Affecter la valeur 0 à l'enum statut => Pour récupérer une valeur d'enum, on utilise le nom de l'enum et la valeur souhaitée
            Status = OrderStatus.Waiting;
            DateOrder = DateTime.Now;
            id = ++count;
        }

        public bool AddProduct(Product product)
        {
            Products.Add(product);
            return true;
        }

        public int Confirm(IPayment payment)
        {
            Payment = payment;
            if(Payment.Pay(Total))
            {
                Status = OrderStatus.Error;
                bool succesCommandInsert = false;
                int succesStatus = 0;
                



                Products.ForEach(p =>
                {
                

                    string request = @"insert into ProductOrders SET price = @price, idOrder = @idOrder, idProduct = @idProduct ";

                    /* ******* */
                    command = new SqlCommand(request, DataBase.Connection);
                    command.Parameters.Add(new SqlParameter("@price",p.Price));
                    command.Parameters.Add(new SqlParameter("@idOrder", this.Id));
                    command.Parameters.Add(new SqlParameter("@idProduct", p.Id));
                    /* ********* */

                    if (command.ExecuteNonQuery() > 0) {
                        /* ********* */
                        p.Stock -= 1;
                        /* ********* */
                        request = @"Update Product set stock = (stock-1) where idProduct = @idProduct ";
                        command = new SqlCommand(request, DataBase.Connection);
                        
                        /* ********* */
                        int result = command.ExecuteNonQuery();

                        command.Dispose();

                        if (result < 1 )
                        {
                            succesCommandInsert = false;
                            return;
                        }


                        /* ********* */
                        request = @"select stock from Product where idProduct = @idProduct ";
                        command = new SqlCommand(request, DataBase.Connection);
                        command.Parameters.Add(new SqlParameter("@idProduct", p.Id));


                        DataBase.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            stock
                        }


                        succesCommandInsert = true;
                        /* ********* */

                    }
                    else
                    {
                        succesCommandInsert = false;

                        request = @"Update Orders set status = @status";
                        command = new SqlCommand(request, DataBase.Connection);
                        command.Parameters.Add(new SqlParameter("@status",OrderStatus.Error));

                        int result = command.ExecuteNonQuery();
                        if (result < 1)
                        {
                            Console.WriteLine("BDD Error ...");                            
                        }

                        return;

                    }
                       

                });

                if (Products.Count > 0 && succesCommandInsert)
                {
                    string request = @"Update Orders set status = @status";
                    command.Parameters.Add(new SqlParameter("@status", Status));

                    command = new SqlCommand(request, DataBase.Connection);

                    object result = command.ExecuteScalar();

                    if ((result.GetType() != typeof(DBNull)) && int.TryParse(result.ToString(), out this.id))
                    {
                        // inserted ID
                        command.Dispose();
                        return Id;
                    }
                    else
                    {
                        command.Dispose();
                        return -1;
                    }
                }
                
            }
            return -1;
        }

        public int save()
        {

            string requete = @"Insert into Orders";
            return 0;
        }



        public override string ToString()
        {
            string response = $"=========Numéro de vente : {Id}==========\n";
            response += "-----Lite produits -----\n";
            Products.ForEach(p =>
            {
                response += p.ToString() + "\n";
            });
            response += $"Total : {Total} euros";
            return response;
        }
    }

    //Création d'une enum pour le statut des ventes
    enum OrderStatus
    {
        Waiting,
        Confirmed,
        Canceled,
        Error,
    }
}

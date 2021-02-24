using System;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Caisse
{
    public class DataProvider
    {


        private static SqlCommand command;
        private static SqlDataReader reader;
        SqlConnection connection;
        string chaineConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ihab\source\repos\CoursMCPDNETF\FichierBaseDeDonneesSqlServer.mdf;Integrated Security=True;Connect Timeout=30";

        public DataProvider()
        {
            connection = new SqlConnection(chaineConnexion);
        }

        public DataProvider(string connectionString)
        {
            chaineConnexion = connectionString;
            connection = new SqlConnection(chaineConnexion);

        }


        public SqlConnection Open()
        {

            connection.Open();
            

            return connection;
        }

        public SqlDataReader GetReader()
        {

            return command.ExecuteReader();
        }

        public void Reset()
        {
            connection.Dispose();
        }

        public void Close()
        {
            connection.Dispose();
            connection.Close();

        }


    }
}

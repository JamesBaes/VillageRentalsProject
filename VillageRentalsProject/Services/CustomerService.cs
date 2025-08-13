using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace VillageRentalsProject.Services
{
    public class CustomerService
    {
        protected MySqlConnection connection;

        public CustomerService()
        {
            string dbHost = "localhost";
            string dbUser = "root";
            string dbPassword = "password";

            MySqlConnectionStringBuilder builder = new()
            {
                Server = dbHost,
                UserID = dbUser,
                Password = dbPassword,
                Database = "restaurant_management_system"
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }


        /// <summary>
        /// Initialize the database for customers
        /// </summary>
        public void InitializeCustomerDb()
        {
            MySqlConnection tempConnection = new MySqlConnection($"Server=localhost;User ID=root;Password=password;");
            tempConnection.Open();

            string dbSql = @"CREATE DATABASE IF NOT EXISTS ;";
            MySqlCommand dbCommand = new MySqlCommand(dbSql, tempConnection);
            dbCommand.ExecuteNonQuery();
            tempConnection.Close();

            connection.Open();

            // Creating table for customers
            string createSql = @"CREATE TABLE IF NOT EXISTS customers (\
                customerId VARCHAR(255) PRIMARY KEY, 
                firstName VARCHAR(255), 
                lastName VARCHAR(255),
                phoneNumber VARCHAR(255),
                email VARCHAR(255
                )";

            MySqlCommand tableCommand = new(createSql, connection);

            tableCommand.ExecuteNonQuery();

            connection.Close();

        }

        
    }
}

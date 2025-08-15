using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using VillageRentalsProject.Models;


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
                Database = "village_rentals"
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

            string dbSql = @"CREATE DATABASE IF NOT EXISTS village_rentals";
            MySqlCommand dbCommand = new MySqlCommand(dbSql, tempConnection);
            dbCommand.ExecuteNonQuery();
            tempConnection.Close();

            connection.Open();

            // Creating table for customers
            string createSql = @"CREATE TABLE IF NOT EXISTS customers (
                customerId VARCHAR(255) PRIMARY KEY, 
                firstName VARCHAR(255), 
                lastName VARCHAR(255),
                email VARCHAR(255),
                phoneNumber VARCHAR(255)
                )";

            MySqlCommand tableCommand = new(createSql, connection);

            tableCommand.ExecuteNonQuery();

            connection.Close();

        }

        /// <summary>
        /// Gets the current list of customers from the database
        /// </summary>
        /// <returns>Returns the current list of customers</returns>
        public List<Customer> GetCustomerList()
        {
            List<Customer> customers = new();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM customers";

                // command object for selecting from the database
                MySqlCommand command = new(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string customerId = reader.GetString(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string email = reader.GetString(3);
                        string phoneNumber = reader.GetString(4);

                        Guid customerGuid = Guid.Parse(customerId);

                        Customer customer = new(customerGuid, firstName, lastName, email, phoneNumber);
                        customers.Add(customer);
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occured {ex.Message}");
                return customers;
            }
            finally
            {
                connection.Close();
            }
        }
        
        /// <summary>
        /// Adds customer to the database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            try
            {
                connection.Open();

                string insertSql = $"INSERT INTO customers (customerId, firstName, lastName, email, phoneNumber) VALUES" +
                    $"('{customer.CustomerId.ToString()}', '{customer.FirstName}', '{customer.LastName}', '{customer.Email}', '{customer.PhoneNumber}');";

                MySqlCommand insertCommand = new(insertSql, connection);

                insertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

        }


        /// <summary>
        /// Removes customer from database
        /// </summary>
        /// <param name="customer"></param>
        public void RemoveCustomer(Customer customer)
        {
            try
            {
                connection.Open();

                string deleteSql = $"DELETE FROM customers WHERE customerId = '{customer.CustomerId}';";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, connection);

                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occured: {ex.Message}");
            }
            finally
            {
                connection.Close(); 
            }
        }
        /// <summary>
        /// Gets the customer from the database by referring to its customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomer(Guid id)
        {
            Customer customer = null;
            try
            {
                connection.Open();

                string selectSql = $"SELECT customerId, firstName, lastName, email, phoneNumber FROM customers WHERE customerId = '{id}';";

                MySqlCommand command = new MySqlCommand(selectSql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string customerId = reader.GetString(0);
                        string firstName = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string email = reader.GetString(3);
                        string phoneNumber = reader.GetString(4);


                        Guid customerGuid = Guid.Parse(customerId);

                        customer = new Customer(customerGuid, firstName, lastName, email, phoneNumber);
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred: {ex.Message}");
                return customer;
            }
            finally
            {
                connection.Close();
            }
        }

    } // class
} // namespace

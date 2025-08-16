using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using VillageRentalsProject.Models;
namespace VillageRentalsProject.Services
{
    public class RentalService
    {
        protected MySqlConnection connection;

        public RentalService()
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
        /// Initialize the database for Rentals
        /// </summary>
        public void InitializeRentalsDb()
        {
            MySqlConnection tempConnection = new MySqlConnection($"Server=localhost;User ID=root;Password=password;");
            tempConnection.Open();

            string dbSql = @"CREATE DATABASE IF NOT EXISTS village_rentals";
            MySqlCommand dbCommand = new MySqlCommand(dbSql, tempConnection);
            dbCommand.ExecuteNonQuery();
            tempConnection.Close();

            connection.Open();

            // Creating table for equipment
            string createSql = @"CREATE TABLE IF NOT EXISTS rentals (
                rentalId VARCHAR(255) PRIMARY KEY, 
                currentDate VARCHAR(255),
                customerId VARCHAR(255), 
                equipmentId VARCHAR(255),
                rentalDate VARCHAR(255),
                returnDate VARCHAR(255),
                cost VARCHAR(255),
                FOREIGN KEY (customerId) REFERENCES customers(customerId),
                FOREIGN KEY (equipmentId) REFERENCES equipment(equipmentId)
                );";

            MySqlCommand tableCommand = new(createSql, connection);

            tableCommand.ExecuteNonQuery();

            connection.Close();

        }

        /// <summary>
        /// process a rental and add it to the rentals database
        /// </summary>
        /// <param name="rental"></param>
        public void AddRental(Rental rental, Customer customer, Equipment equipment)
        {
            try
            {
                connection.Open();

                CustomerService customerService = new();
                EquipmentService equipmentService = new();

                customerService.GetCustomer(customer.CustomerId);
                equipmentService.GetEquipment(equipment.EquipmentId);

                if(rental.CustomerId == customer.CustomerId && rental.EquipmentId == equipment.EquipmentId)
                {
                    string insertSql = $"INSERT INTO rentals (rentalId, currentDate, customerId, equipmentId, rentalDate, returnDate, cost) VALUES" +
                    $"('{rental.RentalId.ToString()}', '{rental.CurrentDate.ToString()}', '{rental.CustomerId.ToString()}', '{rental.EquipmentId.ToString()}', '{rental.RentalDate.ToString()}', '{rental.ReturnDate.ToString()}', '{rental.Cost}');";

                    MySqlCommand insertCommand = new(insertSql, connection);

                    insertCommand.ExecuteNonQuery();
                }

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
        /// Gets the current list of rentals from the database
        /// </summary>
        /// <returns>Returns the current list of rentals</returns>
        public List<Rental> GetRentalsList()
        {
            List<Rental> rentalsList = new();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM rentals";

                // command object for selecting from the database
                MySqlCommand command = new(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string rentalId = reader.GetString(0);
                        string currentDate = reader.GetString(1);
                        string customerId = reader.GetString(2);
                        string equipmentId = reader.GetString(3);
                        string rentalDate = reader.GetString(4);
                        string returnDate = reader.GetString(5);
                        string cost = reader.GetString(6);

                        Guid rentalGuid = Guid.Parse(rentalId);
                        DateTime currentDateParsed = DateTime.Parse(currentDate);
                        Guid equipmentGuid = Guid.Parse(equipmentId);
                        Guid customerGuid = Guid.Parse(customerId);
                        DateTime rentalDateParsed = DateTime.Parse(rentalDate);
                        DateTime returnDateParsed = DateTime.Parse(returnDate);

                        Rental rental = new(rentalGuid, currentDateParsed, customerGuid, equipmentGuid, rentalDateParsed, returnDateParsed, cost);
                        rentalsList.Add(rental);
                       
                    }
                }
                return rentalsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occured {ex.Message}");
                return rentalsList;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Gets the rental from the database by referring to its rentalId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rental GetRental(Guid id)
        {
            Rental rental = null;
            try
            {
                connection.Open();

                string selectSql = $"SELECT rentalId, currentDate, customerId, equipmentId, rentalDate, returnDate, cost FROM rentals WHERE rentalId = '{id}';";

                MySqlCommand command = new MySqlCommand(selectSql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string rentalId = reader.GetString(0);
                        string currentDate = reader.GetString(1);
                        string customerId = reader.GetString(2);
                        string equipmentId = reader.GetString(3);
                        string rentalDate = reader.GetString(4);
                        string returnDate = reader.GetString(5);
                        string cost = reader.GetString(6);

                        Guid rentalGuid = Guid.Parse(rentalId);
                        DateTime currentDateParsed = DateTime.Parse(currentDate);
                        Guid equipmentGuid = Guid.Parse(equipmentId);
                        Guid customerGuid = Guid.Parse(customerId);
                        DateTime rentalDateParsed = DateTime.Parse(rentalDate);
                        DateTime returnDateParsed = DateTime.Parse(returnDate);

                        rental = new(rentalGuid, currentDateParsed, customerGuid, equipmentGuid, rentalDateParsed, returnDateParsed, cost);
                    }
                }

                return rental;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred: {ex.Message}");
                return rental;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Removes rental from database
        /// </summary>
        /// <param name="rental"></param>
        public void RemoveRental(Rental rental)
        {
            try
            {
                connection.Open();

                string deleteSql = $"DELETE FROM rentals WHERE rentalId = '{rental.RentalId}';";

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

    }// class
}// namespace

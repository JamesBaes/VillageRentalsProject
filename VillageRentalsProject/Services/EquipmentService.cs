using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using VillageRentalsProject.Models;

namespace VillageRentalsProject.Services
{
    public class EquipmentService
    {
        protected MySqlConnection connection;

        public EquipmentService()
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
        /// Initialize the database for Equipment Inventory
        /// </summary>
        public void InitializeEquipmentDb()
        {
            MySqlConnection tempConnection = new MySqlConnection($"Server=localhost;User ID=root;Password=password;");
            tempConnection.Open();

            string dbSql = @"CREATE DATABASE IF NOT EXISTS village_rentals";
            MySqlCommand dbCommand = new MySqlCommand(dbSql, tempConnection);
            dbCommand.ExecuteNonQuery();
            tempConnection.Close();

            connection.Open();

            // Creating table for equipment
            string createSql = @"CREATE TABLE IF NOT EXISTS equipment (
                equipmentId VARCHAR(255) PRIMARY KEY, 
                categoryId VARCHAR(255), 
                equipmentName VARCHAR(255),
                status VARCHAR(255),
                dailyRate VARCHAR(255),
                description VARCHAR(255)
                );";

            MySqlCommand tableCommand = new(createSql, connection);

            tableCommand.ExecuteNonQuery();

            connection.Close();

        }

        /// <summary>
        /// Gets the current list of equipment from the database
        /// </summary>
        /// <returns>Returns the current list of equipment</returns>
        public List<Equipment> GetEquipmentList()
        {
            List<Equipment> equipmentList = new();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM equipment";

                // command object for selecting from the database
                MySqlCommand command = new(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string equipmentId = reader.GetString(0);
                        string categoryId = reader.GetString(1);
                        string equipmentName = reader.GetString(2);
                        string status = reader.GetString(3);
                        string dailyRate = reader.GetString(4);
                        string description = reader.GetString(5);

                        Guid equipmentGuid = Guid.Parse(equipmentId);

                        Equipment equipment = new(equipmentGuid, categoryId, equipmentName, status, dailyRate, description);
                        equipmentList.Add(equipment);
                    }
                }
                return equipmentList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occured {ex.Message}");
                return equipmentList;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Adds equipment to the database
        /// </summary>
        /// <param name="equipment"></param>
        public void AddEquipment(Equipment equipment)
        {
            try
            {
                connection.Open();

                string insertSql = $"INSERT INTO equipment (equipmentId, categoryId, equipmentName, status, dailyRate, description) VALUES" +
                    $"('{equipment.EquipmentId.ToString()}', '{equipment.CategoryId}', '{equipment.EquipmentName}', '{equipment.Status}', '{equipment.DailyRate}', '{equipment.Description}');";

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
        /// Removes equipment from database
        /// </summary>
        /// <param name="equipment"></param>
        public void RemoveEquipment(Equipment equipment)
        {
            try
            {
                connection.Open();

                string deleteSql = $"DELETE FROM equipment WHERE equipmentId = '{equipment.EquipmentId.ToString()}';";

                MySqlCommand deleteCommand = new MySqlCommand(deleteSql, connection);

                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error has occured: {ex.Message}");

            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Gets the equipment from the database by referring to its equipmentId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Equipment GetEquipment(Guid id)
        {
            Equipment equipment = null;
            try
            {
                connection.Open();

                string selectSql = $"SELECT equipmentId, categoryId, equipmentName, status, dailyRate, description FROM equipment WHERE equipmentId = '{id}';";

                MySqlCommand command = new MySqlCommand(selectSql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string equipmentId = reader.GetString(0);
                        string categoryId = reader.GetString(1);
                        string equipmentName = reader.GetString(2);
                        string status = reader.GetString(3);
                        string dailyRate = reader.GetString(4);
                        string description = reader.GetString(5);

                        Guid equipmentGuid = Guid.Parse(equipmentId);

                        equipment = new Equipment(equipmentGuid, categoryId, equipmentName, status, dailyRate, description);
                    }
                }

                return equipment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has occurred: {ex.Message}");
                return equipment;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

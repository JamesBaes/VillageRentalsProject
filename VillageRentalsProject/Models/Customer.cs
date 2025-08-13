using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentalsProject.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(string customerId, string firstName, string lastName, string email, string phoneNumber)
        {
            try
            {
                CustomerId = customerId;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Customer: {ex.Message}");
            }
        }


    }
}

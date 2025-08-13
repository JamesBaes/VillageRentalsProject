using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentalsProject.Models
{
    public class Rental
    {
        public Guid RentalId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime RentalDate { get; set; }

        public Rental(Guid rentalId, Guid customerId, DateTime rentalDate)
        {
            RentalId = rentalId;
            CustomerId = customerId;
            RentalDate = rentalDate;
        }

    }
}

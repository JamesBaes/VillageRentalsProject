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
        public Guid EquipmentId { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Cost { get; set; }


        public Rental(Guid rentalId, DateTime currentDate, Guid customerId, Guid equipmentId, DateTime rentalDate, DateTime returnDate, string cost)
        {
            RentalId = rentalId;
            CurrentDate = currentDate;
            CustomerId = customerId;
            EquipmentId = equipmentId;
            RentalDate = rentalDate;
            ReturnDate = returnDate;
            Cost = cost;
        }

    }
}

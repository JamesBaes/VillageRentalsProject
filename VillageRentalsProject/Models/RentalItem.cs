using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentalsProject.Models
{
    public class RentalItem
    {
        public Guid RentalItemId { get; set; }
        public Guid RentalId { get; set; }
        public Guid EquipmentId { get; set; }
        public decimal ItemCost { get; set; }
        
        public RentalItem(Guid rentalItemId, Guid rentalId, Guid equipmentId, decimal itemCost)
        {
            RentalItemId = rentalItemId;
            RentalId = rentalId;
            EquipmentId = equipmentId;
            ItemCost = itemCost;
        }
    }
}

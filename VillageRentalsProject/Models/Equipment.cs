using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentalsProject.Models
{
    public class Equipment
    {
        public Guid EquipmentId { get; set; }
        public string CategoryId { get; set; }
        public string EquipmentName { get; set; }
        public string Status { get; set; }
        public string DailyRate { get; set; }
        public string Description { get; set; }

        public Equipment(Guid equipmentId, string categoryId, string equipmentName, string status, string dailyRate, string description)
        {
            EquipmentId = equipmentId;
            CategoryId = categoryId;
            EquipmentName = equipmentName;
            Status = status;
            DailyRate = dailyRate;
            Description = description;
           
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentalsProject.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }


        public Category(string categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}

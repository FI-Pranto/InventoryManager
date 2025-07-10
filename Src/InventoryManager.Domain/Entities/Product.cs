using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int StockQuantity { get; set; } = 0;
        public double Price { get; set; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int UnitId { get; set; }

        public Unit Unit { get; set; }
    }
}

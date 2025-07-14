using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public decimal TotalAmount { get; set; }

        //navigation properties
        public Supplier Supplier { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }=new List<PurchaseItem>();
    }
}

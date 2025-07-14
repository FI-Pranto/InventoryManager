﻿

namespace InventoryManager.Domain.Entities
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        //navigation properties
        public Product Product { get; set; }
        public Purchase Purchase { get; set; }
    }
}

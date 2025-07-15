using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Domain.Entities
{
    public class MaterialIssueItem
    {
        public int Id { get; set; }

        public int MaterialIssueId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double ProductPrice { get; set; }

        public MaterialIssue MaterialIssue { get; set; }
        public Product Product { get; set; }
    }
}

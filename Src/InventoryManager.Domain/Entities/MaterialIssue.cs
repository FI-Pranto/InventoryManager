using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Domain.Entities
{
    public class MaterialIssue
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime IssueDate { get; set; }

        public double TotalAmount { get; set; }

        public Customer Customer { get; set; }

        public List<MaterialIssueItem> MaterialIssueItems { get; set; } = new List<MaterialIssueItem>();
    }
}

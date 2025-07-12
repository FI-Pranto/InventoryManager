using System.ComponentModel.DataAnnotations;


namespace InventoryManager.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();


    }
}

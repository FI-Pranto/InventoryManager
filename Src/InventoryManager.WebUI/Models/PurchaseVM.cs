using System.ComponentModel.DataAnnotations;
namespace InventoryManager.WebUI.Models
{
    public class PurchaseVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Supplier is required.")]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Purchase Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "At least one product must be added.")]
        [MinLength(1, ErrorMessage = "You must add at least one product.")]
        public List<PurchaseItemVM> PurchaseItems { get; set; } = new List<PurchaseItemVM>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace InventoryManager.WebUI.Models
{
    public class PurchaseItemVM
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}

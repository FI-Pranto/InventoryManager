using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManager.WebUI.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int StockQuantity { get; set; } = 0;
        public double Price { get; set; }

        public IFormFile ImageUri { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> Units { get; set; }
    }
}

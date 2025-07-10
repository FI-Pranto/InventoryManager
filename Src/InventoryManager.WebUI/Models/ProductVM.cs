using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.WebUI.Models
{
    public class ProductVM
    {
        public Product Product { get; set; }

        public IFormFile? ImageUri { get; set; }

    }
}

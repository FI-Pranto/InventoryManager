using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace InventoryManager.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        public int StockQuantity { get; set; } = 0;
        [Range(1, double.MaxValue, ErrorMessage = "Price should not be zero")]
        public double Price { get; set; }


        [ValidateNever]
        public string? ImagePath { get; set; } = null;

        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int UnitId { get; set; }
        [ValidateNever]
        public Unit Unit { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Models
{
    public class ProductViewModel
    {
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display (Name = "Product name")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Image { get; set; }

        [Display(Name = "Active")]
        public bool Status { get; set; }
    }
}

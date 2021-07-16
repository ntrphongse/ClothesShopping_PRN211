using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required]
        [Display (Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}

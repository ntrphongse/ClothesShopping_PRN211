using ClothesShoppingLibrary.DataAccess.Category;
using ClothesShoppingLibrary.DataAccess.OrderDetail;
using System;
using System.Collections.Generic;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess.Product
{
    public partial class ProductDTO
    {
        public ProductDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public int Productd { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}

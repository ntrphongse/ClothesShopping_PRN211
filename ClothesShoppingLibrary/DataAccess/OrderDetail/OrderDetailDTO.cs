using ClothesShoppingLibrary.DataAccess.Order;
using ClothesShoppingLibrary.DataAccess.Product;
using System;
using System.Collections.Generic;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess.OrderDetail
{
    public partial class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public float ItemPrice { get; set; }
        public int Quantity { get; set; }

        public virtual OrderDTO Order { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}

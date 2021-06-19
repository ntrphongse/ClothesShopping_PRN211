using ClothesShoppingLibrary.DataAccess.OrderDetail;
using ClothesShoppingLibrary.DataAccess.User;
using System;
using System.Collections.Generic;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess.Order
{
    public partial class OrderDTO
    {
        public OrderDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public float TotalPrice { get; set; }
        public int NumberOfItem { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual UserDTO Customer { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}

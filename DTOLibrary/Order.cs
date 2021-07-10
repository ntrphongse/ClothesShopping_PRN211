using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DTOLibrary
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        
        public string Address { get; set; }

        [Display(Name = "Total price")]
        public float TotalPrice { get; set; }

        [Display(Name = "Number of Item")]
        public int NumberOfItem { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual User Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

using ClothesShoppingLibrary.DataAccess.Order;
using ClothesShoppingLibrary.DataAccess.Role;
using System;
using System.Collections.Generic;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess.User
{
    public partial class UserDTO
    {
        public UserDTO()
        {
            Orders = new HashSet<OrderDTO>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }

        public virtual RoleDTO RoleNavigation { get; set; }
        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}

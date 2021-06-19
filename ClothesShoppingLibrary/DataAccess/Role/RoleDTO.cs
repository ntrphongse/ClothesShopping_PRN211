using ClothesShoppingLibrary.DataAccess.User;
using System;
using System.Collections.Generic;

#nullable disable

namespace ClothesShoppingLibrary.DataAccess.Role
{
    public partial class RoleDTO
    {
        public RoleDTO()
        {
            Users = new HashSet<UserDTO>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserDTO> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DTOLibrary
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }

        [Display (Name = "Full name")]
        public string FullName { get; set; }

        [Display (Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }



        [Display (Name = "Role")]
        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

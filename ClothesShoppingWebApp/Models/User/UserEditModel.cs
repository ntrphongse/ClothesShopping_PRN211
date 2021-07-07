using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Models.User
{
    public class UserEditModel
    {
        public int? UserId { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Gender")]
        public bool? Gender { get; set; }

        [Display(Name = "Adress")]
        public string? Address { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }

        [Display(Name = "Confirm")]
        [StringLength(100, MinimumLength = 8)]
        public string? Confirm { get; set; }

    }
}

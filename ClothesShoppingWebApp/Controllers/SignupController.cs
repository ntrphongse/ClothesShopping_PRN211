using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index(string? email, string? password)
        {
            return Json($"{email} : {password}");
        }
    }
}

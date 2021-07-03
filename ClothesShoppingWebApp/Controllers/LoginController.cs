using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClothesShoppingWebApp.Controllers
{
    public class LoginController : Controller
    {
        IUserRepository userRepository = null;
        public LoginController()
        {
            userRepository = new UserRepository();
        }
        // GET: Login
        public IActionResult Index()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string email, 
            [FromForm] string password)
        {
            try
            {
                ViewBag.Email = email;
                User loginUser = userRepository.Login(email, password);
                if (loginUser == null)
                {
                    ViewBag.Message = "Login failed";
                } else
                {
                    // Get Session
                    ISession session = HttpContext.Session;

                    // Store Login user to Session
                    session.SetString("LOGIN_USER", loginUser.UserId.ToString());
                    ViewBag.Message = "Login successfully!";

                    // Authorize
                    var userClaims = new List<Claim>()
                    {
                        new Claim("UserId", loginUser.UserId.ToString()),
                        new Claim(ClaimTypes.Name, loginUser.FullName),
                        new Claim(ClaimTypes.Email, loginUser.Email),
                        new Claim(ClaimTypes.DateOfBirth, loginUser.Birthday.ToString()),
                        new Claim(ClaimTypes.Role, loginUser.RoleNavigation.RoleName)
                    };

                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    HttpContext.SignInAsync(userPrincipal);
                }
                return View();
            } catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult UserAccessDenied()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}

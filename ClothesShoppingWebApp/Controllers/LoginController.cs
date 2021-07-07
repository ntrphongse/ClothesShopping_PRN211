using DAOLibrary.Repository.Interface;
using DAOLibrary.Repository.Object;
using DTOLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string email, 
            [FromForm] string password)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                ViewBag.Email = email;
                User loginUser = userRepository.Login(email, password);
                if (loginUser == null)
                {
                    ViewBag.Message = "Login failed";
                } else
                {
                    //// Get Session
                    //ISession session = HttpContext.Session;

                    //// Store Login user to Session
                    //session.SetString("LOGIN_USER", loginUser.UserId.ToString());
                    //ViewBag.Message = "Login successfully!";

                    // Authorize
                    var userClaims = new List<Claim>()
                    {
                        new Claim("UserId", loginUser.UserId.ToString()),
                        new Claim(ClaimTypes.Email, loginUser.Email),
                        new Claim(ClaimTypes.Role, loginUser.RoleNavigation.RoleName)
                    };

                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    HttpContext.SignInAsync(userPrincipal);
                }
                return RedirectToAction("Index", "Home");
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

        [AllowAnonymous]
        public IActionResult Google()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            };
            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var request = HttpContext.Request;
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (!result.Succeeded)
                {
                    throw new Exception("External authentication error");
                }

                var externalUser = result.Principal;
                if (externalUser == null)
                {
                    throw new Exception("External authentication error");
                }

                var email = externalUser.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value;
                IUserRepository _userRepository = new UserRepository();
                User user = _userRepository.GetUser(email);

                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await HttpContext.SignOutAsync();
                    //TempData["Email"] = email;
                    string fullname = externalUser.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value;
                    return RedirectToAction("Index", "Signup", new { email, fullname });
                }
            } catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

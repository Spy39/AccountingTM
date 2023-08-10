using Accounting.Data;
using Accounting.Models;
using AccountingTM.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountingTM.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;

		public AccountController(DataContext context)
		{
			_context = context;
		}

        [HttpGet]
		public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
            if (user != null) 
            {
                Authenticate(user.Login);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == model.Login);
            if (user != null)
            {
                ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
            }
            else
            {
                _context.Users.Add(new User 
                { 
                    Login = model.Login,
                    Name = model.Name,
                    Password = model.Password,
                    Role = model.Role
                });
                _context.SaveChanges();
                return Ok();
            }
            return View(model);
        }

        private void Authenticate(string login)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity)).Wait();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Login");
        }
    }
}

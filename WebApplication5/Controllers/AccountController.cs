using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        public List<Users> users = null;

        public AccountController() 
        { 
            users = new List<Users>();
            users.Add(new Users()
            {
                UserId = 1,
                Email="admin@gmail.com",
                Password="123",
                Role="Admin"
            });
            users.Add(new Users()
            {
                UserId = 2,
                Email = "lykimanh@gmail.com",
                Password = "123",
                Role = "Student"
            });
            users.Add(new Users()
            {
                UserId = 3,
                Email = "manager@gmail.com",
                Password = "123",
                Role = "Manager"
            });
            users.Add(new Users()
            {
                UserId = 4,
                Email = "coordinator@gmail.com",
                Password = "123",
                Role = "Coordinator 1"
            });
            users.Add(new Users()
            {
                UserId = 5,
                Email = "coordinator1@gmail.com",
                Password = "123",
                Role = "Coordinator 2"
            });
            users.Add(new Users()
            {
                UserId = 6,
                Email = "coordinator2@gmail.com",
                Password = "123",
                Role = "Coordinator 3"
            });
            users.Add(new Users()
            {
                UserId = 7,
                Email = "guest@gmail.com",
                Password = "123",
                Role = "Guest"
            });
            users.Add(new Users()
            {
                UserId =8,
                Email = "tranthingochong@gmail.com",
                Password = "123",
                Role = "Student"
            });
            users.Add(new Users()
            {
                UserId = 9,
                Email = "doanhdoanh@gmail.com",
                Password = "123",
                Role = "Student"
            });
            users.Add(new Users()
            {
                UserId = 10,
                Email = "luclongquan@gmail.com",
                Password = "123",
                Role = "Student"
            });
            users.Add(new Users()
            {
                UserId = 11,
                Email = "tungne@gmail.com",
                Password = "123",
                Role = "Student"
            });
            users.Add(new Users()
            {
                UserId = 12,
                Email = "coordinator3@gmail.com",
                Password = "123",
                Role = "Coordinator 4"
            });
            users.Add(new Users()
            {
                UserId = 13,
                Email = "coordinator4@gmail.com",
                Password = "123",
                Role = "Coordinator 5"
            });
        }
        public IActionResult Login(string returnUrl="/")
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = users.Where(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefault();
            if (user != null) 
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim("DotNetMania","Code")
                };

                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    });

                return LocalRedirect(login.ReturnUrl);
            }
            else
            {
                ViewBag.Message = "Invalid Credential";
                return View(login);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Account/Login");
        }
    }
}

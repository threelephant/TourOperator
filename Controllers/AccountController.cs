using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourOperator.ViewModels;
using TourOperator.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace TourOperator.Controllers
{
    public class AccountController : Controller
    {
        private TourOperatorContext db;
        private readonly ILogger<AccountController> logger;
        public AccountController(TourOperatorContext context, ILogger<AccountController> logger)
        {
            db = context;
            this.logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = new Security().Base64Encode(model.Password);
                User user = await db.Users.Include(u => u.Role)
                                          .FirstOrDefaultAsync(u => u.Login == model.Login
                                                            && u.Password == hashedPassword);
                            
                if (user != null)
                {
                    await Authenticate(user);        
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и пароль");
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    var anyData = await db.Users.AnyAsync();
                    long lastId = anyData ? await db.Users.MaxAsync(u => u.UserId) : 0;

                    var hashedPassword = new Security().Base64Encode(model.Password);
                    
                    var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                    User newUser = new User { 
                                            UserId = lastId + 1, 
                                            Login = model.Login, 
                                            Password = hashedPassword, 
                                            FirstName = model.FirstName, 
                                            MiddleName = model.MiddleName, 
                                            LastName = model.LastName,
                                            isAdmin = false,
                                            Role = role
                                          };

                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();

                    await Authenticate(newUser);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и/или пароль");
                }
            }

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
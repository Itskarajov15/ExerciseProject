using ExerciseProject.Core.Contracts;
using ExerciseProject.Core.Models.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExerciseProject.WEB.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var userRegistered = await this.userService.RegisterUser(user);

            if (!userRegistered)
            {
                ModelState.AddModelError(String.Empty, "User with that name already exists");
                return View(user);
            }

            return RedirectToAction("Login", "Accounts");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var userId = await this.userService.UserExists(user);

            if (userId == -1)
            {
                ModelState.AddModelError(String.Empty, "Invalid login credentials");
                return View(user);
            }

            var claims = new List<Claim>
            {
                new Claim("Name", user.Name),
                new Claim("UserId", userId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Accounts");
        }
    }
}
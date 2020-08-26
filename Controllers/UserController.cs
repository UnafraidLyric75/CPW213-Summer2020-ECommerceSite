using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // Map data to user account instance
                UserAccount acc = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };

                // add to database
                _context.UserAccounts.Add(acc);
                await _context.SaveChangesAsync();

                // redirect to home
                return RedirectToAction("Index", "Home");

            }

            return View(reg);
        }

        public IActionResult Login()
        {
            // Check if user already logged in
            if (HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAccount account =
                       await (from u in _context.UserAccounts
                       where (u.Username == model.UsernameOrEmail
                          || u.Email == model.UsernameOrEmail)
                          && u.Password == model.Password
                       select u).SingleOrDefaultAsync();

            if(account == null)
            {
                // credential did not match

                // Custom error msg
                ModelState.AddModelError(string.Empty, "Credential were not found");

                return View(model);
            }

            // Log user into website
            HttpContext.Session.SetInt32("UserId", account.UserId);

            return RedirectToAction("Index", "Home");
        }
    }
}

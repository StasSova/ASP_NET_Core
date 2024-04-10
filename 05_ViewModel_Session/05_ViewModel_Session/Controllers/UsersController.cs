using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _05_ViewModel_Session.Models.DataBase;
using _05_ViewModel_Session.Models.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Cryptography;
using System.Text;
using _05_ViewModel_Session.Services.DataBase;

namespace _05_ViewModel_Session.Controllers
{
    public class UsersController : Controller
    {
        private readonly GuestBookContext _context;
        private readonly IRepository _rep;

        public UsersController(IRepository repository)
        {
            _rep = repository;
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VM_User_Register user)
        {
            if (ModelState.IsValid)
            {

                User us = await _rep.CreateUser(user.Name, user.Email, user.Password);
                if (us == null)
                {
                    return View(user);
                }

                HttpContext.Session.SetString("login", us.Name);
                HttpContext.Session.SetInt32("userId", us.Id);

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }


        // GET: Users/Login
        public IActionResult Login()
        {
#if DEBUG
            VM_User_Login user = new()
            {
                Email = "stassova2017@gmail.com",
                Password = "Stas"
            };
            return View(user);
#endif
            return View();
        }
        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VM_User_Login user)
        {
            if (ModelState.IsValid)
            {
                if (await _rep.GetUsersQuantity() == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(user);
                }
                var _user = await _rep.LoginUser(user.Email, user.Password);
                if (_user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(user);
                }

                HttpContext.Session.SetString("login", _user.Name);
                HttpContext.Session.SetInt32("userId", _user.Id);


                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }


        // GET: Users/ContinueAsGuest
        public IActionResult ContinueAsGuest()
        {
            HttpContext.Session.SetString("login", "Guest");
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
    }
}

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

namespace _05_ViewModel_Session.Controllers
{
    public class UsersController : Controller
    {
        private readonly GuestBookContext _context;

        public UsersController(GuestBookContext context)
        {
            _context = context;
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

                User newUser = new User()
                {
                    Email = user.Email,
                    Name = user.Name,
                };

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + user.Password);

                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                newUser.Password = hash.ToString();
                newUser.Salt = salt;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("login", user.Name);
                HttpContext.Session.SetInt32("userId", newUser.Id);

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
                if (_context.Users.Count() == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(user);
                }
                var _user = await _context.Users.Where(x => x.Email == user.Email).FirstAsync();
                if (_user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(user);
                }

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(_user.Salt + user.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (_user.Password != hash.ToString())
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

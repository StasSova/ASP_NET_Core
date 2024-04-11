using _06_MusicCollection.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace _06_MusicCollection.Controllers
{
    public class UserController : Controller
    {
        // ADMIN
        public IActionResult Index()
        {
            // вывод всех пользователей
            return View();
        }


        // GET: User/Register
        public IActionResult Register()
        {

            return View();
        }
        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Email, Password, PasswordRepeat")] VM_User_Register user)
        {
            if (ModelState.IsValid)
            {
                // добавить контроллер для добавления

                //return RedirectToAction(nameof(Index));
                return View(user);
            }
            return View(user);
        }

        // GET: User/Register
        public IActionResult Login()
        {
            return View();
        }
        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email, Password")] VM_User_Login user)
        {
            if (ModelState.IsValid)
            {
                // добавить контроллер для добавления

                //return RedirectToAction(nameof(Index));
                return View(user);
            }
            return View(user);
        }
    }
}

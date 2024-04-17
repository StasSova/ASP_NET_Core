using _06_MusicCollection.Models.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MusicCollection_BLL.Interfaces.User;

public class UserController : Controller
{
    private readonly IUserAuthentication _authenticationService;
    public UserController(IUserAuthentication authenticationService)
    {
        _authenticationService = authenticationService;
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

            // TODO

            //Регистрация нового пользователя
            var result = await _authenticationService.RegisterUserAsync(user.Email, user.Password, 0);

            if (result != null)
            {
                // Пользователь аутентифицирован, перенаправить на основную страницу или куда-то еще
                HttpContext.Session.SetString("email", user.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким email уже существует");
                return View(user);
            }
        }
        // если админ
        else if (user.Email == "admin" && ModelState["Password"]?.ValidationState == ModelValidationState.Valid)
        {
            //Регистрация нового пользователя администратора
            //Если есть уже такой email будет null
            var result = await _authenticationService.RegisterUserAsync(user.Email, user.Password, 1);

            if (result != null)
            {
                // Пользователь аутентифицирован, перенаправить на основную страницу или куда-то еще
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("admin", user.Email);
                return RedirectToAction("Index", "Home");
            }
        }
        return View(user);
    }

    // GET: User/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: User/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("Email, Password")] VM_User_Login user)
    {
        if (ModelState.IsValid)
        {
            // Проверка входа пользователя
            var result = await _authenticationService.AuthenticateUserAsync(user.Email, user.Password);

            if (result != null)
            {
                // Пользователь аутентифицирован, перенаправить на основную страницу или куда-то еще
                HttpContext.Session.SetString("email", user.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Неправильный email или пароль");
                return View(user);
            }
        }
        // если админ
        else if (user.Email == "admin")
        {
            //Авторизация пользователя администратора
            //Если есть уже такой email будет null
            var result = await _authenticationService.AuthenticateUserAsync(user.Email, user.Password);

            if (result != null)
            {
                // Пользователь аутентифицирован, перенаправить на основную страницу или куда-то еще
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError(string.Empty, "Неправильный email или пароль");
        return View(user);
    }
    // GET: User/Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Register", "User");
    }
}

using _03_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace _03_MVC.Controllers
{
    public class FilmController : Controller
    {
        FilmContext db;
        public FilmController(FilmContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Film> films = this.db.Films;
            return View(films);
        }
    }
}

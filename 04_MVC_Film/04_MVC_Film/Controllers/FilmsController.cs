using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _04_MVC_Film.Models;
using System.Threading;

namespace _04_MVC_Film.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmContext _context;
        private readonly IWebHostEnvironment _environment;

        public FilmsController(FilmContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var filmContext = _context.Films.Include(f => f.Director).Include(f => f.Genre);
            return View(await filmContext.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Director)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Year,DirectorId,GenreId,Description")] Film film, IFormFile newUrl)
        {
            if (film.Description == "Admin")
                ModelState.AddModelError("", "admin - запрещенное описание");


            if (ModelState.IsValid && newUrl != null)
            {
                string path = "/img/" + newUrl.FileName;
                film.ImageUrl = path;
                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await newUrl.CopyToAsync(fileStream);
                }

                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Name", film.DirectorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Name", film.DirectorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Year,ImageUrl,DirectorId,GenreId,Description")] Film film, IFormFile newUrl)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (film.Description == "Admin")
                ModelState.AddModelError("", "admin - запрещенное описание");

            if (newUrl != null)
            {
                string path = "/img/" + newUrl.FileName;
                film.ImageUrl = path;
                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await newUrl.CopyToAsync(fileStream);
                }
            }

            ModelState["ImageUrl"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            ModelState["newUrl"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    //var f = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
                    //if (f == null)
                    //    return NotFound();
                    //f.Name = film.Name;
                    //f.Description = film.Description;
                    //f.GenreId = film.GenreId;
                    //f.DirectorId = film.DirectorId;
                    //f.ImageUrl = film.ImageUrl;
                    //f.Year = film.Year;
                    await _context.Update(film.Id, film);// Стандартный метод
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "Name", film.DirectorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Director)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckDescription(string Description)
        {
            if (Description.Length < 2)
                return Json(false);
            return Json(true);
        }
    }
}

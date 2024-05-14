using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _08_RazorPages_Movie_CRUD.Models;
using Microsoft.Extensions.Hosting;
using _08_RazorPages_Movie_CRUD.Models.Services;

namespace _08_RazorPages_Movie_CRUD.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IRepository context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        [BindProperty]
        public IFormFile newUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.GetFilmById(id.Value);
            if (film == null)
            {
                return NotFound();

            }
            Film = film;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState["newUrl"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                string path = "/img/" + newUrl.FileName;
                Film.ImageUrl = path;
                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await newUrl.CopyToAsync(fileStream);
                }

                await _context.Update(Film.Id, Film);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(Film.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FilmExists(int id)
        {
            return _context.GetFilmById(id) != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _08_RazorPages_Movie_CRUD.Models;
using Microsoft.Extensions.Hosting;

namespace _08_RazorPages_Movie_CRUD.Pages
{
    public class CreateModel : PageModel
    {
        private readonly _08_RazorPages_Movie_CRUD.Models.FilmContext _context;
        private readonly IWebHostEnvironment _environment;
        public CreateModel(_08_RazorPages_Movie_CRUD.Models.FilmContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        [BindProperty]
        public IFormFile newUrl { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState["newUrl"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string path = "/img/" + newUrl.FileName;
            Film.ImageUrl = path;
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await newUrl.CopyToAsync(fileStream);
            }

            _context.Films.Add(Film);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _08_RazorPages_Movie_CRUD.Models;
using _08_RazorPages_Movie_CRUD.Models.Services;

namespace _08_RazorPages_Movie_CRUD.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository _context;

        public DeleteModel(IRepository context)
        {
            _context = context;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

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
            else
            {
                Film = film;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.GetFilmById(id.Value);
            if (film != null)
            {
                Film = film;
                await _context.Delete(film);
            }

            return RedirectToPage("./Index");
        }
    }
}

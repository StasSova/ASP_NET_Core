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
    public class DetailsModel : PageModel
    {
        private readonly IRepository _context;

        public DetailsModel(IRepository context)
        {
            _context = context;
        }

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
    }
}

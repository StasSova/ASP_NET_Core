﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _08_RazorPages_Movie_CRUD.Models;

namespace _08_RazorPages_Movie_CRUD.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly _08_RazorPages_Movie_CRUD.Models.FilmContext _context;

        public DetailsModel(_08_RazorPages_Movie_CRUD.Models.FilmContext context)
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

            var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
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

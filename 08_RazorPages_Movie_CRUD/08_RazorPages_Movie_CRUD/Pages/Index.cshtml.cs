using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _08_RazorPages_Movie_CRUD.Models;

namespace _08_RazorPages_Movie_CRUD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly _08_RazorPages_Movie_CRUD.Models.FilmContext _context;

        public IndexModel(_08_RazorPages_Movie_CRUD.Models.FilmContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Film = await _context.Films.ToListAsync();
        }
    }
}

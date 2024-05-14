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
    public class IndexModel : PageModel
    {
        private readonly IRepository _context;

        public IndexModel(IRepository context)
        {
            _context = context;
        }

        public IList<Film> Film { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Film = (await _context.GetFilms()).ToList();
        }
    }
}

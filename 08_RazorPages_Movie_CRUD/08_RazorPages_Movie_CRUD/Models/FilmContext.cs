using Microsoft.EntityFrameworkCore;

namespace _08_RazorPages_Movie_CRUD.Models
{
    public class FilmContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        public FilmContext(DbContextOptions<FilmContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}

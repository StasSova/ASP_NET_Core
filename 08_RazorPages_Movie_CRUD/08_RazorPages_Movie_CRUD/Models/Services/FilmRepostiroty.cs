
using Microsoft.EntityFrameworkCore;

namespace _08_RazorPages_Movie_CRUD.Models.Services
{
    public class FilmRepository : IRepository
    {
        private readonly FilmContext _context;
        public FilmRepository(FilmContext filmContext)
        {
            _context = filmContext;
        }

        public async Task<Film> GetFilmById(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<Film> Add(Film entity)
        {
            if (entity != null)
            {
                _context.Films.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<bool> Delete(Film entity)
        {
            if (entity != null)
            {
                _context.Films.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<Film>> GetFilms() => await _context.Films.ToListAsync();

        public async Task<Film> Update(int id, Film entity)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                film.Name = entity.Name;
                film.Director = entity.Director;
                film.ImageUrl = entity.ImageUrl;
                film.Description = entity.Description;
                film.Genre = entity.Genre;
                await _context.SaveChangesAsync();
                return film;
            }
            return null;
        }
    }
}

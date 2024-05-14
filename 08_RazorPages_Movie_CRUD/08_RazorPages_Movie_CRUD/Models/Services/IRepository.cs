namespace _08_RazorPages_Movie_CRUD.Models.Services
{
    public interface IRepository
    {
        Task<ICollection<Film>> GetFilms();
        Task<Film> Add(Film entity);
        Task<bool> Delete(Film entity);
        Task<Film> Update(int id, Film entity);
        Task<Film> GetFilmById(int id);
    }
}

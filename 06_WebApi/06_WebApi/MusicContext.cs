using _06_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace _06_WebApi
{
    public class MusicContext : DbContext
    {
        public DbSet<M_Artist> Artists { get; set; }
        public DbSet<M_Genre> Genres { get; set; }
        public DbSet<M_User> Users { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                SaveChanges();
            }
        }
    }
}

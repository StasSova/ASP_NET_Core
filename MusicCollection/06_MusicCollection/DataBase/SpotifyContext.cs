using _06_MusicCollection.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace _06_MusicCollection.DataBase
{
    public class SpotifyContext : DbContext
    {
        public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options)
        {
        }
        public DbSet<M_User> Users { get; set; }
    }
}

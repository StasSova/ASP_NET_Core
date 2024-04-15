using _06_MusicCollection.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _06_MusicCollection.DataBase;

public class SpotifyContext : DbContext
{
    public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options)
    {
    }
    public DbSet<M_Album> Albums { get; set; }
    public DbSet<M_Author> Authors { get; set; }
    public DbSet<M_Genre> Genres { get; set; }
    public DbSet<M_Song> Songs { get; set; }
    public DbSet<M_User> Users { get; set; }
}


public class GenreConfiguration : IEntityTypeConfiguration<M_Genre>
{
    public void Configure(EntityTypeBuilder<M_Genre> builder)
    {
        builder.HasData(
                    new M_Genre { BackgroundColor = "#006450", Name = "rock", Poster = "rock_poster.jpg" },
                    new M_Genre { BackgroundColor = "#148a08", Name = "pop", Poster = "pop_poster.jpg" },
                    new M_Genre { BackgroundColor = "#503750", Name = "hip_hop", Poster = "hiphop_poster.jpg" },
                    new M_Genre { BackgroundColor = "#dc148c", Name = "music", Poster = "music_poster.jpg" },
                    new M_Genre { BackgroundColor = "#006450", Name = "podcasts", Poster = "podcasts_poster.jpg" },
                    new M_Genre { BackgroundColor = "#8400e7", Name = "live events", Poster = "live_events_poster.jpg" },
                    new M_Genre { BackgroundColor = "#1e3264", Name = "made foryou", Poster = "made_foryou_poster.jpg" },
                    new M_Genre { BackgroundColor = "#e8115b", Name = "new releases", Poster = "new_releases_poster.jpg" },
                    new M_Genre { BackgroundColor = "#27856a", Name = "merch", Poster = "merch_poster.jpg" },
                    new M_Genre { BackgroundColor = "#503750", Name = "hip-hop", Poster = "hip-hop_poster.jpg" },
                    new M_Genre { BackgroundColor = "#e1118c", Name = "mood", Poster = "mood_poster.jpg" },
                    new M_Genre { BackgroundColor = "#477d95", Name = "comedy", Poster = "comedy_poster.jpg" },
                    new M_Genre { BackgroundColor = "#dc148c", Name = "educational", Poster = "educational_poster.jpg" },
                    new M_Genre { BackgroundColor = "#af2896", Name = "true crime", Poster = "true_crime_poster.jpg" },
                    new M_Genre { BackgroundColor = "#dc148c", Name = "sports", Poster = "sports_poster.jpg" },
                    new M_Genre { BackgroundColor = "#8d67ab", Name = "charts", Poster = "charts_poster.jpg" },
                    new M_Genre { BackgroundColor = "#d84000", Name = "dance electronic", Poster = "dance_electronic_poster.jpg" },
                    new M_Genre { BackgroundColor = "#d84000", Name = "chill", Poster = "chill_poster.jpg" }
                );
    }

}

public class SongConfiguration : IEntityTypeConfiguration<M_Song>
{
    public void Configure(EntityTypeBuilder<M_Song> builder)
    {
        builder.HasData(
            new M_Song
            {
                Id = 1,
                Title = "Bohemian Rhapsody",
                Poster = "bohemian_rhapsody_poster.jpg",
                FilePath = "bohemian_rhapsody.mp3",
                Duration = TimeSpan.FromMinutes(6),
                UploadDate = DateTime.Now,
                Likes = 1000,
                Plays = 5000,
                Downloads = 2000,
            },
            new M_Song
            {
                Id = 2,
                Title = "Shape of You",
                Poster = "shape_of_you_poster.jpg",
                FilePath = "shape_of_you.mp3",
                Duration = TimeSpan.FromMinutes(4),
                UploadDate = DateTime.Now,
                Likes = 1200,
                Plays = 6000,
                Downloads = 2500,
            }
            // Добавьте другие песни по мере необходимости
        );
    }
}

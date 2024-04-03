using Microsoft.EntityFrameworkCore;

namespace _03_MVC.Models;


public class FilmContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Director> Directors { get; set; }

    public FilmContext(DbContextOptions<FilmContext> options)
           : base(options)
    {
        Database.EnsureDeleted();
        if (Database.EnsureCreated())
        {
            var director1 = new Director { Name = "Quentin Tarantino" };
            var director2 = new Director { Name = "Christopher Nolan" };
            var genre1 = new Genre { Name = "Action" };
            var genre2 = new Genre { Name = "Drama" };

            Directors.AddRange(director1, director2);
            Genres.AddRange(genre1, genre2);

            Films.Add(new Film { ImageUrl = "/img/1.jpg", Name = "Pulp Fiction", Year = 1994, Director = director1, Genre = genre2, Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption." });
            Films.Add(new Film { ImageUrl = "/img/2.jpg", Name = "Inception", Year = 2010, Director = director2, Genre = genre1, Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O." });
            Films.Add(new Film { ImageUrl = "/img/3.jpg", Name = "The Dark Knight", Year = 2008, Director = director2, Genre = genre1, Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice." });
            Films.Add(new Film { ImageUrl = "/img/4.jpg", Name = "Fight Club", Year = 1999, Director = director1, Genre = genre2, Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more." });
            Films.Add(new Film { ImageUrl = "/img/5.jpg", Name = "Django Unchained", Year = 2012, Director = director1, Genre = genre2, Description = "With the help of a German bounty hunter, a freed slave sets out to rescue his wife from a brutal Mississippi plantation owner." });
            Films.Add(new Film { ImageUrl = "/img/6.jpg", Name = "The Shawshank Redemption", Year = 1994, Director = director1, Genre = genre2, Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency." });
            Films.Add(new Film { ImageUrl = "/img/7.jpg", Name = "The Godfather", Year = 1972, Director = director2, Genre = genre2, Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son." });
            Films.Add(new Film { ImageUrl = "/img/8.jpg", Name = "Forrest Gump", Year = 1994, Director = director2, Genre = genre2, Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart." });
            Films.Add(new Film { ImageUrl = "/img/9.jpg", Name = "The Matrix", Year = 1999, Director = director1, Genre = genre1, Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers." });
            Films.Add(new Film { ImageUrl = "/img/10.jpg", Name = "The Lord of the Rings: The Fellowship of the Ring", Year = 2001, Director = director2, Genre = genre1, Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron." });
            SaveChanges();
        }
    }

}

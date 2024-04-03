namespace _03_MVC.Models;

public class Director
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Film> Films { get; set; }
    public override string ToString()
    {
        return Name;
    }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Film> Films { get; set; }
    public override string ToString()
    {
        return Name;
    }
}

public class Film
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public int? DirectorId { get; set; }
    public Director Director { get; set; }
    public int? GenreId { get; set; }
    public Genre Genre { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}

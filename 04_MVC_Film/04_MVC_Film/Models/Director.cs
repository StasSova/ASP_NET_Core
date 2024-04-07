namespace _04_MVC_Film.Models;

public class Director : DbEntity
{
    public string Name { get; set; }
    public ICollection<Film> Films { get; set; }
    public override string ToString()
    {
        return Name;
    }
}
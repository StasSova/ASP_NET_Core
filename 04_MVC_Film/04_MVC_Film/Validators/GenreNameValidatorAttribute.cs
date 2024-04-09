using _04_MVC_Film.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _04_MVC_Film.Validators;

public class GenreNameValidatorAttribute : ValidationAttribute
{
    public static FilmContext FilmContext;
    public override bool IsValid(object? value)
    {
        if (value == null) return false;
        string? strval = value.ToString();

        return !FilmContext.Genres.Any(x => x.Name == strval);
    }
}

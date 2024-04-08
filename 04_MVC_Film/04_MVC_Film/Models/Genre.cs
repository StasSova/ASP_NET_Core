using _04_MVC_Film.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _04_MVC_Film.Models;

public class Genre : DbEntity
{
    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина поля должна быть в пределах 3...30 символов")]
    [Remote(action: "GenreNameValidator", controller: "Genres", ErrorMessage = "Запрещенное имя жанра")]
    [GenreNameValidator(ErrorMessage = "Жанр с таким именем уже существует")]
    public string Name { get; set; }
    public ICollection<Film> Films { get; set; }
    public override string ToString()
    {
        return Name;
    }
}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _04_MVC_Film.Models;

public class Film : DbEntity
{
    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
    [Display(Name = "Название")]
    public string Name { get; set; }

    [Range(15, 60, ErrorMessage = "Возраст должен быть от 16 до 60 лет")]
    public int Year { get; set; }

    public int? DirectorId { get; set; }
    public Director? Director { get; set; }

    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public string? ImageUrl { get; set; }

    [Remote(action: "CheckDescription", controller: "Films", ErrorMessage = "Контролллер вернул ошибку")]
    public string Description { get; set; }
}
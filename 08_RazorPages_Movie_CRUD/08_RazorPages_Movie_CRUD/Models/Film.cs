using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace _08_RazorPages_Movie_CRUD.Models;

public class Film
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
    [Display(Name = "Название")]
    public string Name { get; set; }

    [Range(1900, 2025, ErrorMessage = "Год должен быть от 1900 до 2025 лет")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
    [Display(Name = "Режисер")]
    public string Director { get; set; }

    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; }

    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Поле обязательное к заполнению")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
    [Display(Name = "Описание")]
    public string Description { get; set; }
}
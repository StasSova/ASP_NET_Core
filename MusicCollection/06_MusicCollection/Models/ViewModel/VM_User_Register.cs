using System.ComponentModel.DataAnnotations;

namespace _06_MusicCollection.Models.ViewModel
{
    public class VM_User_Register
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        private const string SpecialCharacters = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[" + SpecialCharacters + @"])(?!.*\s).{8,16}$", ErrorMessage = "Пароль должен содержать как минимум одну цифру, одну прописную и одну заглавную букву, а также один из следующих специальных символов: " + SpecialCharacters)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordRepeat { get; set; }
    }
}

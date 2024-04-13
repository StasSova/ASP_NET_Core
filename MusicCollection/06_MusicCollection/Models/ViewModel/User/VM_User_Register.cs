using System.ComponentModel.DataAnnotations;

namespace _06_MusicCollection.Models.ViewModel.User
{
    public class VM_User_Register
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression("^(admin|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}))$\r\n",
            ErrorMessage = "Адрес электронной почты недействителен. Убедитесь, что он указан в таком формате: example@email.com.")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[@!\""#\$%&'()*+,-:;<=>?@[\\\]^_`{|}~])(?!.*\s).{8,16}$",
            ErrorMessage = @"Пароль должен содержать как минимум одну цифру, одну прописную и одну заглавную букву, а также спец символ.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordRepeat { get; set; }
    }
}

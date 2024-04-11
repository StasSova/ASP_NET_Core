using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _06_MusicCollection.Models.ViewModel
{
    public class VM_User_Login
    {

        [Required(ErrorMessage = "Адрес электронной почты недействителен. Убедитесь, что он указан в таком формате: example@email.com.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Адрес электронной почты недействителен. Убедитесь, что он указан в таком формате: example@email.com.")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        // @"^(?=.*\d)(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[@!\""#\$%&'()*+,-./:;<=>?@[\\\]^_`{|}~])(?!.*\s).{8,16}$"
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,16}$",
        //    ErrorMessage = "Пароль должен содержать как минимум одну цифру, одну прописную и одну заглавную букву, а также один из следующих специальных символов: @!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~")]
        public string Password { get; set; }
    }
}

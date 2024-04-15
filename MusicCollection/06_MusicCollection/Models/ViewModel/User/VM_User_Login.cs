using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace _06_MusicCollection.Models.ViewModel.User
{
    public class VM_User_Login
    {
        [Required(ErrorMessage = "Адрес электронной почты недействителен. Убедитесь, что он указан в таком формате: example@email.com.")]
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"^(admin|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}))$",
            ErrorMessage = "Адрес электронной почты недействителен. Убедитесь, что он указан в таком формате: example@email.com.")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Password { get; set; }


    }
}

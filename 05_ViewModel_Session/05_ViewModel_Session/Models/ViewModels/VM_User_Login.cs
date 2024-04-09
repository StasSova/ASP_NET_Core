using System.ComponentModel.DataAnnotations;

namespace _05_ViewModel_Session.Models.ViewModels
{
    public class VM_User_Login
    {
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Password { get; set; }
    }
}

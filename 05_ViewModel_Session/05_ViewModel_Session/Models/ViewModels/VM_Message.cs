using System.ComponentModel.DataAnnotations;

namespace _05_ViewModel_Session.Models.ViewModels
{
    public class VM_Message
    {

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace _05_ViewModel_Session.Models.DataBase
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
    }
}

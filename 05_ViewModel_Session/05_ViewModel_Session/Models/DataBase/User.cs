namespace _05_ViewModel_Session.Models.DataBase
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public ICollection<Message>? Messages { get; set; }
    }
}

using _05_ViewModel_Session.Models.DataBase;

namespace _05_ViewModel_Session.Services.DataBase;

public interface IRepository
{
    Task<User> CreateUser(string username, string email, string password);
    Task<User> LoginUser(string username, string password);
    Task<Message> CreateReview(int userId, string body, DateTime? date);
    Task<ICollection<Message>> GetMessages();
    Task<bool> ReviewExists(int id);
    Task<int> GetUsersQuantity();
    Task<User> GetUserByEmail(string email);
}

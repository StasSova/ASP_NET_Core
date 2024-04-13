using _06_MusicCollection.DataBase.Models;

namespace _06_MusicCollection.Services.DataBaseService.User
{
    public interface IAuthenticationService
    {
        Task<M_User> AuthenticateUserAsync(string email, string password);
        Task<M_User> RegisterUserAsync(string email, string password);
    }
}

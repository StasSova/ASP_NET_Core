using MusicCollection_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.User
{
    public interface IUserAuthentication
    {
        Task<T_User?> AuthenticateUserAsync(string email, string password);
        Task<T_User?> RegisterUserAsync(string email, string password);
    }
}

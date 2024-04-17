using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.Password
{
    public interface IPasswordHashing
    {
        Task<(string hashedPassword, string salt)> HashPasswordAsync(string userPassword);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string userPassword, string salt);
    }
}

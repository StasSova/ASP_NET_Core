using Microsoft.EntityFrameworkCore;
using MusicCollection_BLL.DTO;
using MusicCollection_BLL.Interfaces.Password;
using MusicCollection_DAL.Interfaces;
using MusicCollection_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.User
{
    public class UserAuthentication : IUserAuthentication
    {
        IUnitOfWork _dbContext { get; set; }
        IPasswordHashing _passwordHashing;
        public UserAuthentication(IUnitOfWork database, IPasswordHashing passwordHashing)
        {
            _dbContext = database;
            this._passwordHashing = passwordHashing;
        }

        public async Task<T_User?> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                M_User user = await _dbContext.Generic.GetFirstAsync<M_User, string>("Email", email);
                if (user != null)
                {
                    bool isValidPassword = await _passwordHashing.VerifyPasswordAsync(user.Password, password, user.Salt);

                    if (isValidPassword)
                    {
                        return new T_User(user);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public async Task<T_User?> RegisterUserAsync(string email, string password)
        {
            try
            {
                M_User existingUser = await _dbContext.Generic.GetFirstAsync<M_User, string>("Email", email);

                if (existingUser == null)
                {
                    (string hashedPassword, string salt) = await _passwordHashing.HashPasswordAsync(password);

                    int statusId = 0;
                    if (email == "admin")
                    {
                        // админ
                        var status = await _dbContext.Generic.GetFirstAsync<M_UserStatus, string>("Status", "Administrator");
                        statusId = status.Id;
                    }
                    else
                    {
                        // обычный
                        var status = await _dbContext.Generic.GetFirstAsync<M_UserStatus, string>("Status", "Inactive");
                        statusId = status.Id;
                    }

                    M_User newUser = new M_User
                    {
                        Email = email,
                        Password = hashedPassword,
                        Salt = salt,
                        StatusId = statusId,
                    };

                    await _dbContext.Generic.Add<M_User>(newUser);

                    return new T_User(newUser);
                }

                return null;

            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}

using _06_MusicCollection.DataBase;
using _06_MusicCollection.DataBase.Models;
using _06_MusicCollection.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace _06_MusicCollection.Services.DataBaseService.User
{
    public class AuthenticationService : IAuthenticationService
    {
        private SpotifyContext _dbContext;
        IPasswordHashingService _passwordHashingService;

        public AuthenticationService(SpotifyContext dbContext, IPasswordHashingService passwordHashingService)
        {
            _dbContext = dbContext;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<M_User> AuthenticateUserAsync(string email, string password)
        {
            M_User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                bool isValidPassword = await _passwordHashingService.VerifyPasswordAsync(user.Password, password, user.Salt);

                if (isValidPassword)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<M_User> RegisterUserAsync(string email, string password)
        {
            M_User existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser == null)
            {
                (string hashedPassword, string salt) = await _passwordHashingService.HashPasswordAsync(password);

                M_User newUser = new M_User
                {
                    Email = email,
                    Password = hashedPassword,
                    Salt = salt
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return newUser;
            }

            return null;
        }
    }
}

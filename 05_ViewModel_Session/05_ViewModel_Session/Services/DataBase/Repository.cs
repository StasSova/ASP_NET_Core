using _05_ViewModel_Session.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Cryptography;
using System.Text;

namespace _05_ViewModel_Session.Services.DataBase
{
    public class Repository : IRepository
    {
        private GuestBookContext _context;
        public Repository(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateReview(int userId, string body, DateTime? date)
        {
            try
            {
                Message mes = new Message()
                {
                    Body = body,
                    UserId = userId,
                    Date = date ?? DateTime.UtcNow,
                };

                _context.Add(mes);
                await _context.SaveChangesAsync();

                return mes;
            }
            catch
            {

                return null;
            }
        }
        public async Task<bool> ReviewExists(int id)
        {
            try
            {
                return _context.Messages.Any(e => e.Id == id);
            }
            catch
            {

                return false;
            }
        }
        public async Task<ICollection<Message>> GetMessages()
        {
            try
            {
                return await _context.Messages.Include(m => m.User).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetMessages Repository" + ex.Message);
                return null;
            }
        }


        public async Task<User> CreateUser(string username, string email, string userPassword)
        {
            try
            {
                User newUser = new User()
                {
                    Name = username,
                    Email = email
                };

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + userPassword);
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                newUser.Password = hash.ToString();
                newUser.Salt = salt;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }
            catch
            {
                return null;
            }
        }
        public async Task<User?> LoginUser(string useremail, string userPassword)
        {
            try
            {
                var us = await _context.Users.Where(x => x.Email == useremail).FirstAsync();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(us.Salt + userPassword);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                return userPassword == hash.ToString()
                    ? us
                    : null;
            }
            catch
            {

                return null;
            }
        }

        public async Task<int> GetUsersQuantity()
        {
            try
            {
                return await _context.Users.CountAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users.Where(x => x.Email == email).FirstAsync();
            }
            catch
            {
                return null;
            }
        }

    }
}

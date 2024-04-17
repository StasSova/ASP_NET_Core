using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollection_BLL.Interfaces.Password
{
    public class PasswordHashing : IPasswordHashing
    {
        // Метод для создания хэша пароля
        public async Task<(string hashedPassword, string salt)> HashPasswordAsync(string userPassword)
        {
            byte[] saltbuf = new byte[16];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(saltbuf);
            }
            string salt = BitConverter.ToString(saltbuf).Replace("-", "");

            byte[] password = Encoding.Unicode.GetBytes(salt + userPassword);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] byteHash = sha256.ComputeHash(password);
                string hashedPassword = BitConverter.ToString(byteHash).Replace("-", "");

                // Возвращаем хэш пароля и соль в виде кортежа
                return (hashedPassword, salt);
            }
        }

        // Метод для проверки пароля
        public async Task<bool> VerifyPasswordAsync(string hashedPassword, string userPassword, string salt)
        {
            // Кодирование пароля в байтовый массив с добавлением соли
            byte[] password = Encoding.Unicode.GetBytes(salt + userPassword);

            // Вычисление хэша
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] byteHash = sha256.ComputeHash(password);
                string calculatedHash = BitConverter.ToString(byteHash).Replace("-", "");
                return calculatedHash == hashedPassword;
            }
        }
    }
}

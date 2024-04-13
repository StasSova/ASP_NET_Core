
namespace _06_MusicCollection.Services.PasswordService
{
    public interface IPasswordHashingService
    {
        Task<(string hashedPassword, string salt)> HashPasswordAsync(string userPassword);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string userPassword, string salt);
    }
}

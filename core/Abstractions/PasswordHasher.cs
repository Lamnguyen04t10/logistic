using System.Security.Cryptography;
using System.Text;

namespace Core.Abstractions
{
    public class PasswordHasher
    {
        // Generate a random salt as a Base64 string
        public static string GenerateSalt(int size = 16)
        {
            var saltBytes = new byte[size];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        // Create a hash using SHA256 with salt
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] passwordWithSalt = new byte[passwordBytes.Length + saltBytes.Length];

                Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
                Buffer.BlockCopy(
                    saltBytes,
                    0,
                    passwordWithSalt,
                    passwordBytes.Length,
                    saltBytes.Length
                );

                byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

                return Convert.ToBase64String(hashBytes);
            }
        }

        // Verify the password
        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var newHash = HashPassword(password, salt);
            return newHash == hashedPassword;
        }
    }
}

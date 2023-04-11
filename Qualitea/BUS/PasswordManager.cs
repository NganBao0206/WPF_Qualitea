using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PasswordManager
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            // Tạo muối ngẫu nhiên
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Tạo băm mật khẩu
            byte[] hash = HashPassword(password, salt);

            // Ghép muối và băm mật khẩu lại với nhau
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Chuyển đổi kết quả sang chuỗi base64
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Chuyển đổi chuỗi base64 sang mảng byte
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Tách muối và băm mật khẩu
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            byte[] hash = new byte[HashSize];
            Array.Copy(hashBytes, SaltSize, hash, 0, HashSize);

            // Tạo băm mật khẩu từ mật khẩu nhập vào
            byte[] testHash = HashPassword(password, salt);

            // So sánh hai băm mật khẩu
            return hash.SequenceEqual(testHash);
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return deriveBytes.GetBytes(HashSize);
            }
        }
    }
}

using System.Security.Cryptography;

namespace AuthAccess.AuthService
{
    public class HashingMethod
    {
      
        private const int SaltSize = 16;

      
        private const int HashSize = 32;

        
        private const int Iterations = 10000;

        public static (string hash, string salt) HashPassword(string password)
        {
            // Generate a salt
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            // Hash the password with the salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            byte[] hashBytes = pbkdf2.GetBytes(HashSize);

            // Convert to base64 strings for storage or comparison
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Convert the stored salt and hash to bytes
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Hash the provided password with the stored salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            byte[] testHash = pbkdf2.GetBytes(HashSize);

            // Compare the results
            for (int i = 0; i < HashSize; i++)
            {
                if (testHash[i] != hashBytes[i])
                {
                    return false; // The password does not match
                }
            }

            return true; // The password matches
        }

    }
}

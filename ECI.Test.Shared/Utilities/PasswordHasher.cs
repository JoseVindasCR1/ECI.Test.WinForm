using System;
using System.Security.Cryptography;

namespace ECI.Test.Shared.Utilities
{
    /// <summary>
    /// Provides secure password hashing and verification functionality using PBKDF2
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        /// <summary>
        /// Hashes a password with a random salt using PBKDF2
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>Base64 encoded hash with salt (salt:hash)</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = HashPassword(password, salt);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a password against a stored hash
        /// </summary>
        /// <param name="password">The password to verify</param>
        /// <param name="storedHash">The stored hash to verify against</param>
        /// <returns>True if the password matches, false otherwise</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (string.IsNullOrEmpty(storedHash))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);

                if (hashBytes.Length != SaltSize + HashSize)
                    return false;

                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                byte[] storedPasswordHash = new byte[HashSize];
                Array.Copy(hashBytes, SaltSize, storedPasswordHash, 0, HashSize);

                byte[] providedPasswordHash = HashPassword(password, salt);

                return CompareBytes(storedPasswordHash, providedPasswordHash);
            }
            catch (Exception)
            {
                // If any exception occurs during verification, return false
                return false;
            }
        }

        /// <summary>
        /// Hashes a password with a specific salt using PBKDF2
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The salt to use for hashing</param>
        /// <returns>The hashed password as byte array</returns>
        private static byte[] HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Compares two byte arrays in constant time to prevent timing attacks
        /// </summary>
        /// <param name="a">First byte array</param>
        /// <param name="b">Second byte array</param>
        /// <returns>True if arrays are equal, false otherwise</returns>
        private static bool CompareBytes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }
    }
}
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GiveCampLondon.Website.Utils
{
    public interface IUserUtility
    {
        string EncryptPassword(string password, string salt, string encryptionKey, string initialisationVector);
    }

    public class UserUtility : IUserUtility
    {
        public string EncryptPassword(string password, string salt, string encryptionKey, string initialisationVector)
        {
            var key = GetHashKey(encryptionKey, salt);
            var iv = Convert.FromBase64String(initialisationVector);
            var encryptedResult = Encrypt(key, iv, GetUtf8BytesForString(password));

            return Convert.ToBase64String(encryptedResult);
        }

        #region encryption helper methods

        private static byte[] Encrypt(byte[] key, byte[] iv, byte[] password)
        {
            var encryptor = new AesManaged
            {
                Key = key,
                IV = iv
            };

            // create a memory stream
            using (var encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (var encrypt = new CryptoStream(
                encryptionStream,
                encryptor.CreateEncryptor(),
                CryptoStreamMode.Write))
                {
                    // Encrypt
                    encrypt.Write(password, 0, password.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();

                    return encryptionStream.ToArray();
                }
            }
        }

        private static byte[] GetUtf8BytesForString(string text)
        {
            var encoder = new UTF8Encoding();
            return encoder.GetBytes(text);
        }

        private static byte[] GetHashKey(string encryptionPassword, string salt)
        {
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionPassword, GetUtf8BytesForString(salt));
            return rfc2898DeriveBytes.GetBytes(16);
        }

        #endregion
    }
}
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace VeiculosVistoria.Models
{
    public class AesEncryption
    {
        private static IConfigurationRoot Configuration;

        static AesEncryption()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<FormBusca>();

            Configuration = builder.Build();
        }

        public static string EncryptString(string plainText)
        {
            string keyString = Configuration["AesKey"];
            byte[] key = Convert.FromBase64String(keyString);

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("Invalid key size. Key must be 128, 192, or 256 bits.");
            }

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.GenerateIV(); // Generate a new IV for each encryption

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length); // Write IV to the beginning of the stream

            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using var swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string DecryptString(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            string keyString = Configuration["AesKey"];
            byte[] key = Convert.FromBase64String(keyString);

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new ArgumentException("Invalid key size. Key must be 128, 192, or 256 bits.");
            }

            using Aes aesAlg = Aes.Create();
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            aesAlg.Key = key;
            aesAlg.IV = iv;

            using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
        public static string GenerateAesKey(int keySize = 256)
        {
            byte[] key = new byte[keySize / 8];
            RandomNumberGenerator.Fill(key);
            return Convert.ToBase64String(key);
        }
    }
}

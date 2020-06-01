using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Anotar.NLog;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace BaseUtils
{
    public class EncryptUtils
    {
        private static readonly string PasswordHash = "P@sSw0rd";
        private static readonly string SaltKey = "S@LT&k3Y";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        private static readonly string _secretKey = "GyFGsJpnsvG2KtnubdRUKve5bG9mVX2e";

        private static IJwtAlgorithm algorithm = new HMACSHA256Algorithm() { };
        private static IJsonSerializer serializer = new JsonNetSerializer();
        private static IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

        private static IDateTimeProvider provider = new UtcDateTimeProvider();
        private static IJwtValidator validator = new JwtValidator(serializer, provider);

        public static string Encode(Dictionary<string, object> payload)
        {
            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, _secretKey);
        }

        public static Dictionary<string, object> Decode(string payload)
        {
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            return decoder.DecodeToObject(payload, _secretKey, true) as Dictionary<string, object>;
        }

        internal static string Encrypt(string plainText)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                byte[] cipherTextBytes;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return plainText;
        }

        internal static string Decrypt(string encryptedText)
        {
            try
            {
                var cipherTextBytes = Convert.FromBase64String(encryptedText);
                
                var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                
                var buffer = new byte[cipherTextBytes.Length];
                int byteCount;
                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(buffer, 0, buffer.Length);
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }

                return Encoding.UTF8.GetString(buffer, 0, byteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return encryptedText;
        }
    }
}

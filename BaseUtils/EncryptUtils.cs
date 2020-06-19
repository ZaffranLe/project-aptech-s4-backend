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

       
        public static string Encrypt(string input, System.Text.Encoding encoding)
        {
            try
            {
                Byte[] stringBytes = encoding.GetBytes(input);
                StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
                foreach (byte b in stringBytes)
                {
                    sbBytes.AppendFormat("{0:X2}", b);
                }
                return sbBytes.ToString();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }

            return null;
        }

        public static string Decrypt(string hexInput, System.Text.Encoding encoding)
        {
            try
            {
                int numberChars = hexInput.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
                }
                return encoding.GetString(bytes);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return null;
        }
    }
}

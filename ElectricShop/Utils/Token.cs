using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Newtonsoft.Json;

namespace ElectricShop.Utils
{
    public class TokenManager
    {
        private static readonly string _secret = "QirJsqTK22Q87CYnqoFHoz5GN2-zAEtVNI8sB2_9FsFmCjIAsLbVIIQhWMNqnu1RKcg_tNZ8ZF1W";//key
        private static readonly int _expire = 60 * 60 * 60 * 600; // seconds

        public static string GenerateToken(UserInfo userInfo, int expire)
        {
            try
            {
                var keySec = _secret;
                if (string.IsNullOrWhiteSpace(AppGlobal.ElectricConfig.Secret))
                {
                    keySec = AppGlobal.ElectricConfig.Secret;
                }
                if (expire <= 0) expire = _expire;
                var provider = new UtcDateTimeProvider();
                var createTime = provider.GetNow();
                var expiredTime = provider.GetNow().AddSeconds(expire);
                var secondsSinceEpoch = UnixEpoch.GetSecondsSince(expiredTime);

                var payload = new Dictionary<string, object>
                {
                    { "UserInfo", userInfo },
                    { "exp", secondsSinceEpoch }
                };

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                JWT.IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

                var token = encoder.Encode(payload, keySec);

                return token;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.ToString(), true);
            }
            return null;
        }

        public static bool ValidateToken(string token, out UserInfo userInfo)
        {
            userInfo = null;
            try
            {
                var keySec = _secret;
                if (string.IsNullOrWhiteSpace(AppGlobal.ElectricConfig.Secret))
                {
                    keySec = AppGlobal.ElectricConfig.Secret;
                }

                JWT.IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var stringToken = decoder.Decode(token, keySec, verify: true);
                var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringToken);
                var userInfoPayload = payLoad["UserInfo"];
                userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoPayload.ToString());
                return true;
            }
            catch (TokenExpiredException)
            {
                Logger.Write("Token has expired: " + token, true);
            }
            catch (SignatureVerificationException)
            {
                Logger.Write("Token has invalid signature: " + token, true);
            }
            return false;
        }
    }
}
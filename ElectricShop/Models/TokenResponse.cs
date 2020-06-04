using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class TokenResponse : IDisposable
    {
        public TokenResponse(string token, UserInfo userInfo)
        {
            Token = token;
            UserInfo = userInfo;
        }
        public string Token { get; set; }
        public UserInfo UserInfo { get; set; }

        public void Dispose()
        {
        }
    }
}
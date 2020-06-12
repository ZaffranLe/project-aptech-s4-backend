using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class TokenResponse : IDisposable
    {
        public TokenResponse(string token, UserInfoResponse userInfo)
        {
            Token = token;
            UserInfo = userInfo;
            ListPermission = new List<string>();
        }
        public string Token { get; set; }
        public UserInfoResponse UserInfo { get; set; }
        public List<string> ListPermission { get; set; }

        public void Dispose()
        {
            ListPermission = null;
        }
    }
}
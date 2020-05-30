using System;
using QuantEdge.Entity.Entities;

namespace QuantEdge.Entity.Object
{
    public class UserInfoAndLoginInfo : IDisposable
    {
        public UserInfo UserInfo { get; set; }
        public UserLogin UserLogin { get; set; }
        public void Dispose()
        {
            UserInfo.Dispose();
            UserInfo = null;
            UserLogin.Dispose();
            UserLogin = null;
        }
    }
}

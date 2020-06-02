using System.Collections.Generic;

namespace ElectricShop.Models
{
    public class WebapiCnf
    {
        public string Secret { get; set; }

        public List<UserWebapi> ListUser { get; set; } //user

        public string Host { get; set; }
        public string VHost { get; set; }
        public string Exchange { get; set; }
        public string RounterKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }

        public void Dispose()
        {
        }

        public static string FileName()
        {
            return typeof(WebapiCnf).Name;
        }

    }

    public class UserWebapi
    {
        public string username { get; set; } //user
        public string password { get; set; } //pass
        public string IP { get; set; } //pass
        public string grant_type { get; set; }
        public int expired { get; set; }

        public void Dispose()
        {
        }
    }
}
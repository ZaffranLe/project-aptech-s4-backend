using System;

namespace ElectricShop.Models
{
    public class UserRegistry : IDisposable
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(UserRegistry).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
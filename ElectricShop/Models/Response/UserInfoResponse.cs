using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class UserInfoResponse : IDisposable
    {
        public UserInfo UserInfo { get; set; }
        public  List<Image> ListImages { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(UserInfoResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
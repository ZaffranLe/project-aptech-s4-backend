using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class RoleResponse
    {
        public Role Role { get; set; }
        public List<Permission> ListPermission { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(RoleResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
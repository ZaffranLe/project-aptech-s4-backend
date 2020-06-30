using System;
using System.Collections.Generic;

namespace ElectricShop.Models
{
    public class RolePermissionReq : IDisposable
    {
        public int IdPermission { get; set; }
        public int IdRole { get; set; }
        public int Action { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(RolePermissionReq).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
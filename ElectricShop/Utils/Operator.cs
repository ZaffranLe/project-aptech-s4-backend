using System;
using System.Security.Cryptography;
using System.Text;
using ElectricShop.Common.Enum;

namespace ElectricShop.Utils
{
    public class Operator
    {
        public static readonly string SPECIAL_CHARS = "~@#$%^()_[]{}";

        public static bool HasPermision(int userId, RoleDefinition role)
        {
            if (Memory.Memory.DicUserPermission.ContainsKey(userId))
            {
                if (Memory.Memory.DicUserPermission[userId].Contains(role.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        
    }

    
}

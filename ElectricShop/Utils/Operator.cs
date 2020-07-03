using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ElectricShop.Common.Enum;

namespace ElectricShop.Utils
{
    public class Operator
    {
        public static readonly string SPECIAL_CHARS = "~@#$%^()_[]{}";

        public static bool HasPermision(int userId, RoleDefinitionEnum role)
        {
            if (role == RoleDefinitionEnum.None)
                return true;
            if (Memory.Memory.DicUserPermission.ContainsKey(userId))
            {
                if (Memory.Memory.DicUserPermission[userId].Any(x => x.Name == role.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        
    }

    
}

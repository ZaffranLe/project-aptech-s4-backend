using System.Collections.Generic;
using Anotar.NLog;
using ElectricShop.Entity.Entities;
using ElectricShop.Entity.Keys;

namespace ElectricShop.Memory
{
    public partial class MemoryInfo : ElectricShop.Memory.Memory
    {
        public static List<string> GetListPermission(int userId)
        {
            if (DicUserPermission != null && DicUserPermission.ContainsKey(userId))
            {
                return DicUserPermission[userId];
            }
            return  new List<string>();
        }

    }
}



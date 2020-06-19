using System;
using System.Collections.Generic;

namespace ElectricShop.Models
{
    public class ConfirmOrder : IDisposable
    {
        public int OrderId { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(ConfirmOrder).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
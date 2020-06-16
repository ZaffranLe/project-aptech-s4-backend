using System;

namespace ElectricShop.Models
{
    public class NewOrderReq : IDisposable
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ListProductId { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(NewOrderReq).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class NewProductReq : IDisposable
    {
        public Product Product { get; set; }
        public List<Property> Properties{ get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(NewProductReq).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
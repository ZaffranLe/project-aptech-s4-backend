using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class ProviderResponse : IDisposable
    {
        public Provider Provider{ get; set; }
        public List<Image> ListImages { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(ProviderResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class ProductResponse : IDisposable
    {
        public Product Product { get; set; }
        public  List<Image> ListImages { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(ProductResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
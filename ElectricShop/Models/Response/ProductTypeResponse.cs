using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class ProductTypeResponse : IDisposable
    {
        public ProductType ProductType { get; set; }
        public  List<string> ListImagesUrl { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(ProductTypeResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
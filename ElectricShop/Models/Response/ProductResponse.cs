﻿using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class ManufacturerResponse : IDisposable
    {
        public Manufacturer Manufacturer { get; set; }
        public  List<Image> ListImages { get; set; }

        public void Dispose()
        {
        }
        public static string EntityName()
        {
            return typeof(ManufacturerResponse).Name;
        }

        public static string GetName()
        {
            return EntityName();
        }

    }
}
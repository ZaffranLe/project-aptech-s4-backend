using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class ImportReceiptResponse : ImportReceipt
    {
        public string ProviderName { get; set; }
        public  string EmployeeName { get; set; }

    }
}
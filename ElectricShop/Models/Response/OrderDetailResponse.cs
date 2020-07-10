using System;
using System.Collections.Generic;
using ElectricShop.Entity.Entities;

namespace ElectricShop.Models
{
    public class OrderDetailResponse : OrderDetail
    {
        public  string EmployeeName { get; set; }
        public  string CustomerName { get; set; }

    }
}
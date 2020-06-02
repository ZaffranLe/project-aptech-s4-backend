using System;

namespace ElectricShop.Models
{
    public class AccountInfo : IDisposable
    {
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Hold { get; set; }

        public void Dispose()
        {
            AccountNo = null;
            AccountName = null;
            Status = null;
            Currency = null;
            Balance = null;
            Hold = null;
        }
    }
}
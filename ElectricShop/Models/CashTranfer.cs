using System;

namespace ElectricShop.Models
{
    public class CashTranfer : IDisposable
    {
        public string AccountCredit { get; set; } // tai khoan ghi co
        public string AccountDebit { get; set; } // tai khoann ghi no
        public decimal? TransAmount { get; set; } // gia tri
        public string Currency { get; set; } // don vi tien te
        public string BranchCode { get; set; } // ma chi nhanh
        public int? SeqNo { get; set; } // id tu tang
        public string Remark { get; set; } // ghi chu

        public void Dispose()
        {
            AccountCredit = null;
            AccountDebit = null;
            TransAmount = null;
            Currency = null;
            BranchCode = null;
            SeqNo = null;
            Remark = null;
        }
    }
}
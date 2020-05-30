using System;

namespace ElectricShop.Entity.Keys
{
    public class ContractNoticeDateKeys
    {
        public long UserId { get; set; }

        public long SymbolId { get; set; } 

        public int ContractNoticeType { get; set; } // lại cảnh báo. type enum

        public DateTime CurrentNotifyDay { get; set; } // ngay tao canh bao.

        public override int GetHashCode()
        {
            unchecked
            {
                int result = UserId.GetHashCode();
                result = (result * 397) ^ SymbolId.GetHashCode();
                result = (result * 397) ^ ContractNoticeType.GetHashCode();
                result = (result * 397) ^ CurrentNotifyDay.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ContractNoticeDateKeys)
            {
                var item = obj as ContractNoticeDateKeys;
                return UserId == item.UserId && SymbolId == item.SymbolId && ContractNoticeType == item.ContractNoticeType && CurrentNotifyDay == item.CurrentNotifyDay;
            }
            return false;
        }
    }
}

namespace ElectricShop.Entity.Keys
{
    public class OpenPositionDetailKeys
    {
        #region Properties

        public long MemberId { get; set; }

        public long BaseSymbolId { get; set; }

        public long SymbolId { get; set; }

        public long OrderTransactionId { get; set; }
        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = SymbolId.GetHashCode();
                result = (result * 397) ^ BaseSymbolId.GetHashCode();
                result = (result * 397) ^ MemberId.GetHashCode();
                result = (result * 397) ^ OrderTransactionId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is OpenPositionDetailKeys)
            {
                var item = obj as OpenPositionDetailKeys;
                return SymbolId == item.SymbolId && 
                    BaseSymbolId == item.BaseSymbolId && 
                    OrderTransactionId == item.OrderTransactionId &&
                    MemberId == item.MemberId;
            }
            return false;
        }
    }
}

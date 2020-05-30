namespace ElectricShop.Entity.Keys
{
    public class OpenPositionKeys
    {
        #region Properties

        public long MemberId { get; set; }

        public long BaseSymbolId { get; set; }

        public long SymbolId { get; set; }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = SymbolId.GetHashCode();
                result = (result * 397) ^ BaseSymbolId.GetHashCode();
                result = (result * 397) ^ MemberId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is OpenPositionKeys)
            {
                var item = obj as OpenPositionKeys;
                return SymbolId == item.SymbolId && BaseSymbolId == item.BaseSymbolId && MemberId == item.MemberId;
            }
            return false;
        }
    }
}
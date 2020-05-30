namespace ElectricShop.Entity.Keys
{
    public class MarginManualStatusKeys
    {
        #region Properties

        public long MemberId { get; set; }

        public long BaseSymbolId { get; set; }

        public long SymbolId { get; set; }

        public long ParentSessionId { get; set; }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = MemberId.GetHashCode();
                result = (result * 397) ^ BaseSymbolId.GetHashCode();
                result = (result * 397) ^ SymbolId.GetHashCode();
                result = (result * 397) ^ ParentSessionId.GetHashCode();

                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is MarginManualStatusKeys)
            {
                var item = obj as MarginManualStatusKeys;
                return MemberId == item.MemberId && BaseSymbolId == item.BaseSymbolId && SymbolId == item.SymbolId && ParentSessionId == item.ParentSessionId;
            }
            return false;
        }
    }
}
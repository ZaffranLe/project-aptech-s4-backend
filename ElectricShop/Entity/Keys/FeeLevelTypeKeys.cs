namespace ElectricShop.Entity.Keys
{
    public class FeeLevelTypeKeys
    {
        #region Properties

        public long MemberId { get; set; }
        public long BaseSymbolId { get; set; }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = MemberId.GetHashCode();
                result = (result * 397) ^ BaseSymbolId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is FeeLevelTypeKeys)
            {
                var item = obj as FeeLevelTypeKeys;
                return MemberId == item.MemberId && BaseSymbolId == item.BaseSymbolId;
            }
            return false;
        }
    }
}

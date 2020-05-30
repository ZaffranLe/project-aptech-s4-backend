namespace ElectricShop.Entity.Keys
{
    public class MemberSymbolMappingKeys
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
                result = (result*397) ^ BaseSymbolId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is MemberSymbolMappingKeys)
            {
                var item = obj as MemberSymbolMappingKeys;
                return MemberId == item.MemberId && BaseSymbolId == item.BaseSymbolId;
            }
            return false;
        }
    }
}
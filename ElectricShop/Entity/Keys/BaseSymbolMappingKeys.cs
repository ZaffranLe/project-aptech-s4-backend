namespace ElectricShop.Entity.Keys
{
    public class BaseSymbolMappingKeys
    {
        #region Properties

        public int BrokerId { get; set; }


        public long BaseSymbolId { get; set; }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = BrokerId.GetHashCode();
                result = (result*397) ^ BaseSymbolId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseSymbolMappingKeys)
            {
                var item = obj as BaseSymbolMappingKeys;
                return BrokerId == item.BrokerId && BaseSymbolId == item.BaseSymbolId;
            }
            return false;
        }
    }
}
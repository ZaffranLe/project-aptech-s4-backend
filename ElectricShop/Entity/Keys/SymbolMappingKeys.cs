namespace ElectricShop.Entity.Keys
{
    public class SymbolMappingKeys
    {
        public int BrokerId { get; set; }

        public long SymbolId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = BrokerId.GetHashCode();
                result = (result * 397) ^ SymbolId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is SymbolMappingKeys)
            {
                var item = obj as SymbolMappingKeys;
                return BrokerId == item.BrokerId && SymbolId == item.SymbolId;
            }
            return false;
        }
    }
}

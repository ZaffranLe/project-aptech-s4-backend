
namespace ElectricShop.Entity.Keys
{
    public class SymbolSettlementPriceDailyKeys
    {
        public long ParentSessionId { get; set; }

        public long SymbolId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = ParentSessionId.GetHashCode();
                result = (result * 397) ^ SymbolId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is SymbolSettlementPriceDailyKeys)
            {
                var item = obj as SymbolSettlementPriceDailyKeys;
                return ParentSessionId == item.ParentSessionId && SymbolId == item.SymbolId;
            }
            return false;
        }
    }
}
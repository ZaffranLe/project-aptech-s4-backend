namespace ElectricShop.Entity.Keys
{
    public class EQ_AssignedSymbolKeys
    {
        public string AccountNumber { get; set; } //Key 
        public string MasterCode { get; set; } //Key
        public string Exchange { get; set; } //Key

        public override int GetHashCode()
        {
            unchecked
            {
                int result = AccountNumber.GetHashCode();
                result = (result * 397) ^ MasterCode.GetHashCode();
                result = (result * 397) ^ Exchange.GetHashCode();
                return result;
            }
        }
        public override bool Equals(object obj)
        {

            if (obj is EQ_AssignedSymbolKeys)
            {
                var item = obj as EQ_AssignedSymbolKeys;
                return AccountNumber == item.AccountNumber && MasterCode == item.MasterCode && Exchange == item.Exchange;
            }
            return false;
        }
    }
}

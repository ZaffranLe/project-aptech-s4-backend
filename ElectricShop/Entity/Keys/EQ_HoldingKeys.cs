namespace ElectricShop.Entity.Keys
{
    public class EQ_HoldingKeys
    {
        public string AccountNumber { get; set; } //Key 
        public string Exchange { get; set; } //Key 
        public string SecurityCode { get; set; } //Key 

        public override int GetHashCode()
        {
            unchecked
            {
                int result = AccountNumber.GetHashCode();
                result = (result * 397) ^ Exchange.GetHashCode();
                result = (result * 397) ^ SecurityCode.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is EQ_HoldingKeys)
            {
                var item = obj as EQ_HoldingKeys;
                return AccountNumber == item.AccountNumber && Exchange == item.Exchange && SecurityCode == item.SecurityCode;
            }
            return false;
        }
    }
}

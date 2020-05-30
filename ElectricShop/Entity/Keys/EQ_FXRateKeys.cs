namespace ElectricShop.Entity.Keys
{
    public class EQ_FXRateKeys
    {
        public string FromCurrency { get; set; } //Key 
        public string ToCurrency { get; set; } //Key

        public override int GetHashCode()
        {
            unchecked
            {
                int result = FromCurrency.GetHashCode();
                result = (result * 397) ^ ToCurrency.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {

            if (obj is EQ_FXRateKeys)
            {
                var item = obj as EQ_FXRateKeys;
                return FromCurrency == item.FromCurrency && ToCurrency == item.ToCurrency;
            }
            return false;
        }
    }
}

namespace ElectricShop.Entity.Keys
{
    public class EQ_OrderMappingKeys
    {
        public string TransactionId { get; set; } //Key 
        public string BrokerOrderId { get; set; } //Key

        public override int GetHashCode()
        {
            unchecked
            {
                int result = TransactionId.GetHashCode();
                result = (result * 397) ^ BrokerOrderId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {

            if (obj is EQ_OrderMappingKeys)
            {
                var item = obj as EQ_OrderMappingKeys;
                return TransactionId == item.TransactionId && BrokerOrderId == item.BrokerOrderId;
            }
            return false;
        }
    }
}

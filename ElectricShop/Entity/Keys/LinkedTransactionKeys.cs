namespace ElectricShop.Entity.Keys
{
    public class LinkedTransactionKeys
    {
        public long OrderTransactionBuyId { get; set; }
        public long OrderTransactionSellId { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = OrderTransactionBuyId.GetHashCode();
                result = (result * 397) ^ OrderTransactionSellId.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is LinkedTransactionKeys)
            {
                var item = obj as LinkedTransactionKeys;
                return OrderTransactionSellId == item.OrderTransactionSellId && OrderTransactionBuyId == item.OrderTransactionBuyId;
            }
            return false;
        }
    }
}
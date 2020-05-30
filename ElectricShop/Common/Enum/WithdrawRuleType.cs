namespace ElectricShop.Common.Enum
{
    public enum WithdrawRuleType
    {
        OriginalCurrency = 1, //Rút tiền trên nguyên tệ


        DepositTotalConvert = 2, //Nộp trên tổng tiền qui đổi


        DepositTotalConvert_Original = 3 //Nộp trên tổng tiền qui đổi. Tối đa nguyên tệ
    }
}
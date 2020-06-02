namespace ElectricShop.Common.Enum
{
    public enum EntityGet
    {
        GetAllValues = 1, //Lấy all thông tin
        GetCustomValues = 2, //Lấy theo custom ( sử dụng sql command text)
        GetCustomStore = 3
    }
}
using System;

namespace ElectricShop.Entity.Keys
{
    public class HolidayExceptionKeys
    {
        #region Properties

        public long HolidayTypeId { get; set; }

        public DateTime HolidayDate { get; set; }

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                int result = HolidayTypeId.GetHashCode();
                result = (result * 397) ^ HolidayDate.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is HolidayExceptionKeys)
            {
                var item = obj as HolidayExceptionKeys;
                return HolidayDate == item.HolidayDate && HolidayTypeId == item.HolidayTypeId;
            }
            return false;
        }
    }
}
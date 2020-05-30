using System;
using System.Data;
using System.IO;

namespace ElectricShop.Entity.Entities
{
    public class YdspLme : BaseEntity
    {
        #region InnerClass

        public enum YdspLmeFields
        {
            BussinessDate,
            FileLme,
            Price,
            TimeChanged
        }

        public enum YdspLmeKey
        {
            FileLme
        }
        #endregion

        #region Contructor

        public YdspLme()
        {
        }

        #endregion

        #region Properties

        public DateTime? BussinessDate { get; set; }
        public string FileLme { get; set; } //Key 
        public string Price { get; set; }
        public DateTime? TimeChanged { get; set; }

        #endregion

        #region Validation

        public override bool IsValid()
        {
            if (FileLme == null)
                throw new NoNullAllowedException("Field: FileLme in entity: YdspLme is Null");
            if (FileLme != null && FileLme.Trim() == String.Empty)
                throw new InvalidDataException("Field: FileLme in entity: YdspLme is Empty");
            if (FileLme != null && FileLme.Length > 200)
                throw new InvalidDataException("Field: FileLme in entity: YdspLme is over-size: 200, value=" + FileLme);

            if (Price != null && Price.Length > 2147483647)
                throw new InvalidDataException("Field: Price in entity: YdspLme is over-size: 2147483647, value=" + Price);
            return true;
        }

        #endregion

        #region EntityName, GetName

        public static string EntityName()
        {
            return typeof(YdspLme).Name;
        }

        public override string GetName()
        {
            return EntityName();
        }

        #endregion

        #region Custom Method
        #endregion


    }
}


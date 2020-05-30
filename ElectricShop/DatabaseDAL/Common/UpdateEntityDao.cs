using System;
using System.Collections.Generic;
using ElectricShop.Entity;

namespace ElectricShop.DatabaseDAL.Common
{
    public class UpdateEntityDao : IDisposable
    {
        #region Data Members

        private UpdateEntitySql _dataObject;
        #endregion

        #region Constructor

        public UpdateEntityDao()
        {
                _dataObject = new UpdateEntitySql();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntityActionCommand"></param>
        /// <returns></returns>
        public bool UpdateDefault(List<EntityCommand> listEntityActionCommand)
        {
            return _dataObject.UpdateDefault(listEntityActionCommand);
        }

        #endregion

        public void Dispose()
        {
            if (_dataObject != null)
                _dataObject.Dispose();
            _dataObject = null;

          
        }
    }
}

using System.Collections.Generic;

namespace CommonicationMemory.Resources
{
    public class UpdateEntityDao
    {
        #region Data Members

        private readonly UpdateEntitySql _dataObject;

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
        public bool Update(List<EntityCommand> listEntityActionCommand)
        {
            return _dataObject.Update(listEntityActionCommand);
        }

        public List<BaseEntity> Select(EntityQuery entityQuery)
        {
            if (entityQuery.QueryAction == EntityGet.GetAllValues)
            {
                return _dataObject.GetAllEntity(entityQuery);
            }
            return null;
        }

        #endregion
    }
}

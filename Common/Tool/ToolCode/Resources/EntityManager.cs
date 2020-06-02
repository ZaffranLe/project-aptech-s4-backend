namespace CommonicationMemory.Resources
{
    public class EntityManager
    {
        #region Instance

        public static EntityManager Instance
        {
            get { return Nested.Instance; }
        }

        private class Nested
        {
            internal static readonly EntityManager Instance = new EntityManager();
        }

        #endregion

        public EntityBaseSql GetMyEntity(string entityName)
        {

            if (entityName.Equals(MemberInfo.ENTITY_NAME))
            {
                return new MemberInfoSql();
            }
           
            return null;
        }
    }
}

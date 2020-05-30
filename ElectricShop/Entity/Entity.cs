using ElectricShop.Entity.Entities;

namespace ElectricShop.Entity
{
    public class Entity
    {
        public string EntityName { get; set; }

        public Entity()
        {
        }

        public Entity(BaseEntity entity)
        {
            SetEntity(entity);
        }

        public BaseEntity GetEntity()
        {
            if (string.IsNullOrWhiteSpace(EntityName)) return null;
            var prop = this.GetType().GetProperty(EntityName);
            if (prop == null) return null;
            return prop.GetValue(this, null) as BaseEntity;
        }

        public void SetEntity(BaseEntity entity)
        {
            var typeName = entity.GetType().Name;
            var myPropInfo = GetType().GetProperty(typeName);
            if (myPropInfo != null)
            {
                myPropInfo.SetValue(this, entity, null);
                EntityName = typeName;
            }
        }

        public string GetName()
        {
            return EntityName;
        }
		
#region MyObject

#endregion


#region MyView
#endregion


#region MyEntity

public YdspLme YdspLme { get; set; }
#endregion

    }
}


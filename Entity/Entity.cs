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

public Customer Customer { get; set; }
public Image Image { get; set; }
public ImportReceipt ImportReceipt { get; set; }
public Manufacturer Manufacturer { get; set; }
public OrderDetail OrderDetail { get; set; }
public Permission Permission { get; set; }
public Product Product { get; set; }
public ProductType ProductType { get; set; }
public Property Property { get; set; }
public Provider Provider { get; set; }
public Role  Role{ get; set; }
public RolePermission  RolePermission{ get; set; }
public UserInfo  UserInfo{ get; set; }
public UserLogin  UserLogin{ get; set; }
public UserRole  UserRole{ get; set; }
   
#endregion

    }
}


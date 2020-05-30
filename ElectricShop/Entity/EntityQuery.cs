using System;
using System.Collections.Generic;
using ElectricShop.Common.Enum;
using ElectricShop.Common.Object;

namespace ElectricShop.Entity
{
    public class EntityQuery : ICloneable
    {
        public string EntityName { get; set; }

        public EntityGet QueryAction { get; set; }

        public List<Entity> ReturnValue { get; set; }

        public DescriptorColection DescriptorColection { get; set; } //Sử dụng đối với QueryAction = GetCustomValues
        
        public bool IsGetMaxKey { get; set; } //Có lấy thêm MaxKey hay không?
        public object ReturnMaxKey { get; set; } //thông tin max key
        public bool IsNotGetValue { get; set; } //= true thì chỉ lấy max key mà không lấy dữ liệu
        public long ItemsCount { get; set; } //Nếu set giá trị Count thì sẽ lấy top số lượng bản ghi
        public string ItemsSort { get; set; } //Nếu set giá trị Count thì sẽ order theo trường này
        public string SortType { get; set; } //Nếu set giá trị Count thì sẽ order theo trường này

        public string StoreName { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public static EntityQuery RequestAllByName(string entityName)
        {
            //Trả về EntityQuerry theo Name truyền vào
            var entityQry = new EntityQuery
            {
                EntityName = entityName,
                QueryAction = EntityGet.GetAllValues
            };
            return entityQry;
        }
    }
}
using System.Collections.Generic;

namespace ElectricShop.Common.Object
{
    public class DescriptorColection
    {
        public DescriptorColection()
        {
            Descriptors = new List<Descriptor>();
            ListField = new List<string>();
        }

        public LogicalOperator Logical { get; set; }

        public List<string> ListField { get; set; } //Cac field can query (Dung cho custom query)

        public List<Descriptor> Descriptors { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantEdge.Entity.Entities;

namespace QuantEdge.Worker.Common.DatabaseDAL.Common
{
    public class IndexDataReaderUtils
    {
        public static Dictionary<string, Dictionary<string, int>> _dicIndexEntity = new Dictionary<string, Dictionary<string, int>>();

        public static void LoadIndex(IDataReader dataReader, string entityName)
        {
            if (!_dicIndexEntity.ContainsKey(entityName))
                _dicIndexEntity[entityName] = new Dictionary<string, int>();
        }
         


        public static Dictionary<string, int> _dicIndex = new Dictionary<string, int>();
        public static void FillDicIndex(IDataReader dataReader)
        {
            var lstStringField = Enum.GetNames(typeof(ViewDealSyncMurex.ViewDealSyncMurexFields));
            foreach (var name in lstStringField)
            {
                int index = dataReader.GetOrdinal(name);
                _dicIndex[name] = index;
            }
        }

        public static int GetIndex(string name)
        {
            return _dicIndex[name];
        }
    }
}

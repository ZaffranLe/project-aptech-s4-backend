using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantEdge.Entity.Keys
{
    public class %EntityName%Keys
    {
        #region Properties

        %KeysProperties%

        #endregion

        public override int GetHashCode()
        {
            unchecked
            {
                %KeysHashCode%
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is %EntityName%Keys)
            {
                var item = obj as %EntityName%Keys;
                return %KeysEquals%;
            }
            return false;
        }
    }
}

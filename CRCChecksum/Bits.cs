using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCChecksum
{
    public class Bits : List<bool>, ICloneable
    {
        public object Clone()
        {
            Bits bits = new Bits();
            for(int i = 0; i < this.Count; i++)
            {
                bits.Add(this[i]);
            }
            return bits;
        }

        public override string ToString()
        {
            string res = "";
            for(int i = 0; i < Count; i++)
            {
                res += this[i] ? "1" : "0";
            }
            return res;
        }
    }
}

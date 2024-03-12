using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCChecksum
{
    public static class CRC
    {
        public static Bits GetChecksum(Polynom polynom, Bits bits, bool addZeros = true)
        {
            bits = (Bits)bits.Clone();
            if(addZeros)
                for(int i = 0; i < polynom.Grad; i++)
                        bits.Add(false);
            int offset = 0;
            while (bits.Count - offset > polynom.Grad && offset >= 0)
            {
                bits = XOR(bits, polynom.Coefficients, offset);
                offset = GetFirstOne(bits);
            }
            Bits res = new Bits();
            for(int i = bits.Count - polynom.Grad; i < bits.Count; i++)
            {
                res.Add(bits[i]);
            }
            return res;
        }

        private static Bits XOR(Bits bits1, Bits bits2, int offset)
        {
            Bits res = new Bits();
            for(int i = 0; i < bits1.Count; i++)
            {
                if (i < offset || i >= offset + bits2.Count)
                {
                    res.Add(bits1[i]);
                    continue;
                }
                res.Add(bits1[i] ^ bits2[i - offset]);
            }
            return res;
        }

        private static int GetFirstOne(Bits bits)
        {
            for (int i = 0; i < bits.Count; i++)
                if (bits[i])
                    return i;
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCChecksum
{
    public class Polynom
    {
        static string numsUp = "⁰¹²³⁴⁵⁶⁷⁸⁹";

        public readonly int Grad = 0;
        public readonly Bits Coefficients;

        private Polynom(int grad, Bits koeff)
        {
            Grad = grad;
            Coefficients = koeff;
        }

        public static Polynom Parse(string polynom)
        {
            int max = 0;
            List<int> coeffs = new List<int>();
            for (int i = 0; i < polynom.Length; i++)
            {
                int ind = numsUp.IndexOf(polynom[i]);
                if (ind > -1)
                {
                    if (ind > max) max = ind;
                    if (coeffs.Contains(ind)) throw new Exception("No other coefficient except 0 and 1 allowed!");
                    coeffs.Add(ind);
                }
            }
            int grad = max;
            Bits coeffsB = new Bits();
            for(int i = grad; i >= 0; i--)
            {
                coeffsB.Add(coeffs.Contains(i));
            }
            return new Polynom(grad, coeffsB);
        }
    }
}

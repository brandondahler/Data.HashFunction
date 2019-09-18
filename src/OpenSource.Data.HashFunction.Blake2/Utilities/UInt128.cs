using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Blake2.Utilities
{

    internal struct UInt128
    {
        public UInt64 Low { get; }
        public UInt64 High { get; }

        
        public UInt128(UInt64 low)
            : this(low, 0)
        {

        }

        public UInt128(UInt64 low, UInt64 high)
        {
            Low = low;
            High = high;
        }


        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            var carryOver = 0UL;
            var lowResult = unchecked(a.Low + b.Low);

            if (lowResult < a.Low)
                carryOver = 1UL;


            return new UInt128(lowResult, a.High + b.High + carryOver);
        }

    }
}

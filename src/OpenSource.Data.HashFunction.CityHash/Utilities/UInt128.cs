using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.CityHash.Utilities
{
    internal struct UInt128
    {
        public UInt64 Low { get; }
        public UInt64 High { get; }

        public UInt128(UInt64 low)
            : this(low, 0UL)
        {

        }

        public UInt128(UInt64 low, UInt64 high)
        {
            Low = low;
            High = high;
        }
    }
}

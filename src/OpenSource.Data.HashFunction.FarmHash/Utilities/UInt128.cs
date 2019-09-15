using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash.Utilities
{
    internal struct UInt128
    {
        public UInt64 Low { get; set; }
        public UInt64 High { get; set; }
        

        public UInt128(UInt64 low, UInt64 high )
        {
            Low = low;
            High = high;
        }
    }
}

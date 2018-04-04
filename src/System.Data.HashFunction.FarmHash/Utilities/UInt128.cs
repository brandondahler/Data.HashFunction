using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.Utilities
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


        public byte[] GetBytes()
        {
            var dataArray = new byte[16];

            Array.Copy(BitConverter.GetBytes(Low), 0, dataArray, 0, 8);
            Array.Copy(BitConverter.GetBytes(High), 0, dataArray, 8, 8);

            return dataArray;
        }
    }
}

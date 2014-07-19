using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Class for storing FNV prime and offset combinations.
    /// 
    /// Values specified as System.Numerics.BigInteger, converted to collections of UInt32 values.
    /// </summary>
    public class FNVPrimeOffset
    {
        /// <summary>
        /// FNV prime number as a IReadOnlyList of UInt32 values.
        /// </summary>
        public virtual IReadOnlyList<UInt32> Prime { get; private set; }

        /// <summary>
        /// FNV prime number as a IReadOnlyList of UInt32 values.
        /// </summary>
        public virtual IReadOnlyList<UInt32> Offset { get; private set; }


        /// <summary>
        /// Constructs new <see cref="FNVPrimeOffset"/> instance.
        /// </summary>
        /// <param name="bitSize">Number of bits the prime and offset use each.</param>
        /// <param name="prime">Prime integer to be represented.</param>
        /// <param name="offset">Offset integer to be represented.</param>
        public FNVPrimeOffset(int bitSize, BigInteger prime, BigInteger offset)
        {
            Prime = prime.ToUInt32Array(bitSize);
            Offset = offset.ToUInt32Array(bitSize);
        }

     }
}

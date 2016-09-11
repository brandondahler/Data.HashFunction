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
#if !NET40
        /// <summary>
        /// FNV prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public virtual IReadOnlyList<UInt32> Prime { get; private set; }
#else
        /// <summary>
        /// FNV prime number as an <see cref="IList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The prime number as an <see cref="IList{UInt32}" />.
        /// </value>
        public virtual IList<UInt32> Prime { get; private set; }
#endif

#if !NET40
        /// <summary>
        /// FNV offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public virtual IReadOnlyList<UInt32> Offset { get; private set; }
#else
        /// <summary>
        /// FNV offset as an <see cref="IList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The offset as an <see cref="IList{UInt32}" />.
        /// </value>
        public virtual IList<UInt32> Offset { get; private set; }
#endif


        /// <summary>
        /// Initializes a new instance of the <see cref="FNVPrimeOffset"/> class.
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

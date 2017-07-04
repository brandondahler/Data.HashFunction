using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.FNV.Utilities;
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
        /// FNV prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public virtual IReadOnlyList<UInt32> Prime { get; private set; }

        /// <summary>
        /// FNV offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public virtual IReadOnlyList<UInt32> Offset { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FNVPrimeOffset"/> class.
        /// </summary>
        /// <param name="bitSize">Number of bits the prime and offset use each.</param>
        /// <param name="prime">Prime integer to be represented.</param>
        /// <param name="offset">Offset integer to be represented.</param>
        public FNVPrimeOffset(int bitSize, BigInteger prime, BigInteger offset)
        {
            Prime = ToUInt32Array(prime, bitSize);
            Offset = ToUInt32Array(offset, bitSize);
        }


        private static UInt32[] ToUInt32Array(BigInteger value, int bitSize)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (bitSize < 0 || bitSize % 32 != 0)
                throw new ArgumentOutOfRangeException(nameof(bitSize), "bitSize must be a positive a multiple of 32.");


            var uint32Values = new UInt32[bitSize / 32];
            var bigIntegerBytes = value.ToByteArray();


            var copyLength = uint32Values.Length * 4;

            if (bigIntegerBytes.Length < copyLength)
                copyLength = bigIntegerBytes.Length;


            Buffer.BlockCopy(
                bigIntegerBytes, 0,
                uint32Values, 0,
                copyLength);

            return uint32Values;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of Fowler–Noll–Vo hash function (FNV-1) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html. 
    /// </summary>
    public class FNV1
        : FNV1Base
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FNV1"/> class.
        /// </summary>
        /// <inheritdoc cref="FNV1Base()" />
        public FNV1()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FNV1"/> class.
        /// </summary>
        /// <inheritdoc cref="FNV1Base(int)" />
        public FNV1(int hashSize)
            : base(hashSize)
        {

        }


        /// <inheritdoc />
        protected override void ProcessBytes(
#if NET45
            ref UInt32[] hash, IReadOnlyList<UInt32> prime, byte[] data, int position, int length)
#else
            ref UInt32[] hash, IList<UInt32> prime, byte[] data, int position, int length)
#endif
        {
            for (var x = position; x < position + length; ++x)
            {
                hash = hash.ExtendedMultiply(prime);
                hash[0] ^= data[x];
            }

        }

        /// <inheritdoc />
        protected override void ProcessBytes32(ref UInt32 hash, UInt32 prime, byte[] data, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                hash *= prime;
                hash ^= data[x];
            }
        }

        /// <inheritdoc />
        protected override void ProcessBytes64(ref UInt64 hash, UInt64 prime, byte[] data, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                hash *= prime;
                hash ^= data[x];
            }
        }

    }
}

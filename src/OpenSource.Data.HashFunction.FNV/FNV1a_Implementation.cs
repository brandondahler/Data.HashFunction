using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.FNV
{
    /// <summary>
    /// Implementation of Fowler–Noll–Vo hash function (FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html. 
    /// </summary>
    internal class FNV1a_Implementation
        : FNV1Base, 
            IFNV1a
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FNV1a_Implementation"/> class.
        /// </summary>
        /// <inheritdoc cref="FNV1Base(IFNVConfig)" />
        public FNV1a_Implementation(IFNVConfig config)
            : base(config)
        {

        }


        /// <inheritdoc />
        protected override void ProcessBytes(ref UInt32[] hash, IReadOnlyList<UInt32> prime, byte[] data, int position, int length)
        {
            var hashSizeInBytes = HashSizeInBits / 8;

            for (var x = position; x < position + length; ++x)
            {
                hash[0] ^= data[x];
                hash = ExtendedMultiply(hash, prime, hashSizeInBytes);
            }
        }

        /// <inheritdoc />
        protected override void ProcessBytes32(ref UInt32 hash, UInt32 prime, byte[] data, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                hash ^= data[x];
                hash *= prime;
            }
        }

        /// <inheritdoc />
        protected override void ProcessBytes64(ref UInt64 hash, UInt64 prime, byte[] data, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                hash ^= data[x];
                hash *= prime;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
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
        /// Creates new <see cref="FNV1"/> instance.
        /// </summary>
        public FNV1()
            : base()
        {

        }


        /// <inheritdoc/>
        protected override IReadOnlyList<UInt32> ProcessBytes(IReadOnlyList<UInt32> prime, IReadOnlyList<UInt32> offset, IReadOnlyList<byte> data)
        {
            var hash = offset.ToArray();

            foreach (var b in data)
            {
                hash = hash.ExtendedMultiply(prime);
                hash[0] ^= b;
            }

            return hash;
        }

        /// <inheritdoc/>
        protected override UInt32 ProcessBytes32(UInt32 prime, UInt32 offset, IReadOnlyList<byte> data)
        {
            var hash = offset;

            foreach (var b in data)
            {
                hash *= prime;
                hash ^= b;
            }

            return hash;
        }

        /// <inheritdoc/>
        protected override UInt64 ProcessBytes64(UInt64 prime, UInt64 offset, IReadOnlyList<byte> data)
        {
            var hash = offset;

            foreach (var b in data)
            {
                hash *= prime;
                hash ^= b;
            }

            return hash;
        }

    }
}

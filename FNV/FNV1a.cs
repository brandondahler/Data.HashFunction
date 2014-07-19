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
    /// Implementation of Fowler–Noll–Vo hash function (FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html. 
    /// </summary>
    public class FNV1a
        : FNV1Base
    {
        /// <summary>
        /// Creates new <see cref="FNV1a"/> instance.
        /// </summary>
        public FNV1a()
            : base()
        {
    
        }


        /// <inheritdoc/>
        protected override IReadOnlyList<UInt32> ProcessBytes(
            IReadOnlyList<UInt32> prime, IReadOnlyList<UInt32> offset, Stream data)
        {
            var hash = offset.ToArray();

            foreach (var dataByte in data.AsEnumerable())
            {
                hash[0] ^= dataByte;
                hash = hash.ExtendedMultiply(prime);
            }

            return hash;
        }

        /// <inheritdoc/>
        protected override UInt32 ProcessBytes32(UInt32 prime, UInt32 offset, Stream data)
        {
            var hash = offset;
            
            foreach (var dataByte in data.AsEnumerable())
            {
                hash ^= dataByte;
                hash *= prime;
            }

            return hash;
        }

        /// <inheritdoc/>
        protected override UInt64 ProcessBytes64(UInt64 prime, UInt64 offset, Stream data)
        {
            var hash = offset;
           
            foreach (var dataByte in data.AsEnumerable())
            {
                    hash ^= dataByte;
                    hash *= prime;
            }

            return hash;
        }
    }
}

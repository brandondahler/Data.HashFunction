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
        protected override UInt32[] ProcessBytes(
            UInt32[] hash, IReadOnlyList<UInt32> prime, byte[] data)
        {
            foreach (var dataByte in data)
            {
                hash = hash.ExtendedMultiply(prime);
                hash[0] ^= dataByte;
            }

            return hash;
        }

        /// <inheritdoc />
        protected override UInt32 ProcessBytes32(UInt32 hash, UInt32 prime, byte[] data)
        {
            foreach (var dataByte in data)
            {
                hash *= prime;
                hash ^= dataByte;
            }

            return hash;
        }

        /// <inheritdoc />
        protected override UInt64 ProcessBytes64(UInt64 hash, UInt64 prime, byte[] data)
        {
            foreach (var dataByte in data)
            {
                hash *= prime;
                hash ^= dataByte;
            }

            return hash;
        }

    }
}

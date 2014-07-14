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
    /// Implementation of Fowler–Noll–Vo hash function (FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html. 
    /// </summary>
    public class FNV1a
        : FNV1Base
    {
        /// <summary>
        /// Creates new <see cref="FNV1a"/> instance.
        /// </summary>
        /// <remarks>HashSize defaults to 64 bits.</remarks>
        public FNV1a()
            : base(64)
        {
    
        }
        

        /// <inheritdoc/>
        public override byte[] ComputeHash(byte[] data)
        {
            if (!HashParameters.ContainsKey(HashSize))
                throw new ArgumentOutOfRangeException("HashSize");

            if (HashSize == 32)
                return ComputeHash32(data);
            else if (HashSize == 64)
                return ComputeHash64(data);


            // Initialize hash with the offset, make copy.
            var hash = HashParameters[HashSize].Offset.ToArray();

            if (hash.Length != HashSize / 32)
                throw new ArgumentOutOfRangeException("HashParameters[HashSize].Offset.ToArray().Length");

            // Look up prime, do not make copy.
            var prime = HashParameters[HashSize].Prime;

            if (prime.Count != HashSize / 32)
                throw new ArgumentOutOfRangeException("HashParameters[HashSize].Prime.Count");


            foreach (var b in data)
            {
                hash[0] ^= b;
                hash = hash.ExtendedMultiply(prime);
            }

            return hash
                .ToBytes()
                .ToArray();
        }


        /// <summary>
        /// 32-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>4-byte array containing the results of hashing the data provided.</returns>
        private byte[] ComputeHash32(byte[] data)
        {
            var hashPrime = HashParameters[32].Prime[0];
            var hash =      HashParameters[32].Offset[0];

            foreach (var b in data)
            {
                hash ^= b;
                hash *= hashPrime;
            }

            return BitConverter.GetBytes(hash);
        }


        /// <summary>
        /// 64-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>8-byte array containing the results of hashing the data provided.</returns>
        private byte[] ComputeHash64(byte[] data)
        {
            var hashPrime = ((UInt64) HashParameters[64].Prime[1]  << 32) | HashParameters[64].Prime[0];
            var hash =      ((UInt64) HashParameters[64].Offset[1] << 32) | HashParameters[64].Offset[0];

            foreach (var b in data)
            {
                hash ^= b;
                hash *= hashPrime;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}

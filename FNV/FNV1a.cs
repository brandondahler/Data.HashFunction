using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class FNV1a
        : FNVBase
    {
        
        public FNV1a()
            : base(64)
        {
    
        }
        

        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize == 32)
                return ComputeHash32(data);
            else if (HashSize == 64)
                return ComputeHash64(data);

            if (!PrecomputedOffsets.ContainsKey(HashSize) || !PrecomputedPrimes.ContainsKey(HashSize))
                throw new ArgumentOutOfRangeException("HashSize");

            // Initialize hash with the offset, make copy since hash will be modified.
            var hash = new UInt32[HashSize / 32];
            Buffer.BlockCopy(PrecomputedOffsets[HashSize], 0, hash, 0, hash.Length * 4);

            // Look up prime, do not make copy.
            // WARNING: modifying prime will modify the stored prime.
            var prime = PrecomputedPrimes[HashSize];

            foreach (var b in data)
            {
                hash[0] ^= b;
                ExtendedMultiply(hash, prime);
            }

            return UInt32sToBytes(hash);
        }


        private byte[] ComputeHash32(byte[] data)
        {
            var hash =      PrecomputedOffsets[32][0];
            var hashPrime =  PrecomputedPrimes[32][0];

            foreach (var b in data)
            {
                hash ^= b;
                hash *= hashPrime;
            }

            return BitConverter.GetBytes(hash);
        }

        private byte[] ComputeHash64(byte[] data)
        {
            var hash =      ((UInt64) PrecomputedOffsets[64][1] << 32) | PrecomputedOffsets[64][0];
            var hashPrime = ((UInt64)  PrecomputedPrimes[64][1] << 32) |  PrecomputedPrimes[64][0];

            foreach (var b in data)
            {
                hash ^= b;
                hash *= hashPrime;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}

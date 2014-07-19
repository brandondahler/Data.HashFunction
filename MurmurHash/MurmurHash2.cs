using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of MurmurHash2 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash2.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash2.
    /// 
    /// This hash function has been superseded by MurmurHash3.
    /// </summary>
    public class MurmurHash2
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32, 64 }; }
        }

        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        public UInt64 Seed { get; set; }


        /// <inheritdoc/>
        protected override bool RequiresSeekableStream { get { return true; } }        
        
        /// <summary>
        /// Constant as defined by MurmurHash2 specification.
        /// </summary>
        protected const UInt64 MixConstant = 0xc6a4a7935bd1e995;


        /// <summary>
        /// Constructs new <see cref="MurmurHash2"/> instance.
        /// </summary>
        public MurmurHash2()
            : base(64)
        {
            Seed = 0;
        }


        /// <inheritdoc/>
        protected override byte[] ComputeHashInternal(Stream data)
        {
            switch (HashSize)
            {
                case 32:
                    return ComputeHash32(data);

                case 64:
                    return ComputeHash64(data);

                default:
                    throw new ArgumentOutOfRangeException("HashSize");
            }

        }


        /// <summary>
        /// Computes 32-bit hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>Hash value of the data.</returns>
        protected byte[] ComputeHash32(Stream data)
        {
            const UInt32 m = unchecked((UInt32) MixConstant);

            // Initialize the hash to a 'random' value

            UInt32 h = (UInt32) Seed ^ (UInt32) data.Length;
            var dataGroups = data.AsGroupedStreamData(4);


            // Mix 4 bytes at a time into the hash
            foreach (var dataGroup in dataGroups)
            {
                UInt32 k = BitConverter.ToUInt32(dataGroup, 0);

                k *= m;
                k ^= k >> 24;
                k *= m;

                h *= m;
                h ^= k;
            }
            

            // Handle the last few bytes of the input array
            var remainder = dataGroups.Remainder;

            switch(remainder.Length)
            {
                case 3: h ^= (UInt32) remainder[2] << 16; goto case 2;
                case 2: h ^= (UInt32) remainder[1] << 8; goto case 1;
                case 1:
                    h ^= remainder[0];
                    h *= m;
                    break;
            };

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return BitConverter.GetBytes(h);
        }

        /// <summary>
        /// Computes 64-bit hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>Hash value of the data.</returns>
        protected byte[] ComputeHash64(Stream data)
        {
            const UInt64 m = MixConstant;

            UInt64 h = Seed ^ ((UInt64) data.Length * m);
            var dataGroups = data.AsGroupedStreamData(8);


            foreach (var dataGroup in dataGroups)
            {
                UInt64 k = BitConverter.ToUInt64(dataGroup, 0);

                k *= m;
                k ^= k >> 47;
                k *= m;
   
                h ^= k;
                h *= m;
            }


            var remainder = dataGroups.Remainder;

            switch (remainder.Length)
            {
                case 7: h ^= (UInt64) remainder[6] << 48;  goto case 6;
                case 6: h ^= (UInt64) remainder[5] << 40;  goto case 5;
                case 5: h ^= (UInt64) remainder[4] << 32;  goto case 4;
                case 4: h ^= (UInt64) remainder[3] << 24;  goto case 3;
                case 3: h ^= (UInt64) remainder[2] << 16;  goto case 2;
                case 2: h ^= (UInt64) remainder[1] <<  8;  goto case 1;
                case 1: 
                    h ^= (UInt64) remainder[0];
                    h *= m;
                    break;
            };
 
            h ^= h >> 47;
            h *= m;
            h ^= h >> 47;

            return BitConverter.GetBytes(h);
        }
    }
}

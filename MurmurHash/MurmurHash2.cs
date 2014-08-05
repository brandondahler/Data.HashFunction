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
        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The seed value for hash calculation.
        /// </value>
        public UInt64 Seed { get { return _Seed; } }


        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="MurmurHash2" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <inheritdoc/>
        protected override bool RequiresSeekableStream { get { return true; } }

        /// <summary>
        /// Constant as defined by MurmurHash2 specification.
        /// </summary>
        protected const UInt64 MixConstant = 0xc6a4a7935bd1e995;


        private readonly UInt64 _Seed;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 32, 64 };


        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0.  <inheritdoc cref="MurmurHash2(UInt64)" />
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(UInt64)" />
        public MurmurHash2()
            : this(0U)
        {
            
        }

        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(int, UInt64)" />
        public MurmurHash2(int hashSize)
            : this(hashSize, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 64.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(int, UInt64)" />
        public MurmurHash2(UInt64 seed)
            : this(64, seed)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash2"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" /></param>
        /// <param name="seed"><inheritdoc cref="Seed" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within MurmurHash2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public MurmurHash2(int hashSize, UInt64 seed)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within MurmurHash2.ValidHashSizes.");

            _Seed = seed;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            switch (HashSize)
            {
                case 32:
                    return ComputeHash32(data);

                case 64:
                    return ComputeHash64(data);

                default:
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }

        }


        /// <summary>
        /// Computes 32-bit hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>
        /// Hash value of the data.
        /// </returns>
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
        /// <returns>
        /// Hash value of the data.
        /// </returns>
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
